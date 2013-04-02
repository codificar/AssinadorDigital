using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FileUtils
{
    public class FileHistory
    {
        public string OriginalPath;
        public string NewPath;

        public FileHistory(string originalPath, string newPath)
        {
            OriginalPath = originalPath;
            NewPath = newPath;
        }
    }

    public static class FileOperations
    {
        #region PrivateProperties

        static string originalPath;
        private static List<string> allowedFiles = new List<string>();
        private static List<string> commomPaths = new List<string>();

        #endregion

        #region PublicMethods

        public static List<FileStatus> Copy(List<FileHistory> originalFiles, string target, bool overwrite)
        {
            if (!(target.EndsWith("\\")))
                target += "\\";

            //set the root folder that contains the files
            originalPath = Path.GetDirectoryName(originalFiles[0].OriginalPath);
            foreach (FileHistory file in originalFiles)
            {
                string newPath = Path.GetDirectoryName(file.OriginalPath);
                if (originalPath.Length > newPath.Length)
                    originalPath = newPath;
            }

            List<FileStatus> report = new List<FileStatus>();
            List<string> unauthorizedPath = new List<string>();

            foreach (FileHistory fileToCopy in originalFiles)
            {
                string targetPath;
                bool parentPathUnauthorizedAccess = false;
                string subfolderPath = (Path.GetDirectoryName(fileToCopy.OriginalPath)).Substring(originalPath.Length);
                if (subfolderPath == "")
                {
                    targetPath = target;
                }
                else
                {
                    if (subfolderPath.StartsWith("\\"))
                        subfolderPath = subfolderPath.Substring(1);
                    targetPath = target + subfolderPath + "\\";
                }

                targetPath += Path.GetFileName(fileToCopy.OriginalPath);

                foreach (string parentPath in unauthorizedPath)
                {
                    if ((parentPath + "\\") == targetPath.Substring(0, parentPath.Length + 1))
                        parentPathUnauthorizedAccess = true;
                }

                if (!parentPathUnauthorizedAccess)
                {
                    try
                    {
                        if (File.Exists(targetPath))
                            if (overwrite)
                            {
                                if (fileToCopy.OriginalPath == fileToCopy.NewPath)
                                {
                                    File.Copy(fileToCopy.OriginalPath, targetPath, true);
                                }
                                else
                                {
                                    if (fileToCopy.NewPath != targetPath)
                                    {
                                        File.Copy(fileToCopy.NewPath, targetPath, true);
                                        File.Delete(fileToCopy.NewPath);
                                    }
                                }
                                report.Add(new FileStatus(fileToCopy.OriginalPath, targetPath, Status.Success));
                            }
                            else
                            {
                                report.Add(new FileStatus(fileToCopy.OriginalPath, targetPath, Status.Unmodified));
                            }
                        else
                        {
                            if (fileToCopy.OriginalPath == fileToCopy.NewPath)
                            {
                                File.Copy(fileToCopy.OriginalPath, targetPath, false);
                            }
                            else
                            {
                                if (fileToCopy.NewPath != targetPath)
                                {
                                    File.Copy(fileToCopy.NewPath, targetPath, false);
                                    File.Delete(fileToCopy.NewPath);
                                }
                            }
                            report.Add(new FileStatus(fileToCopy.OriginalPath, targetPath, Status.Success));
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        unauthorizedPath.Add(Path.GetDirectoryName(targetPath));
                        report.Add(new FileStatus(fileToCopy.OriginalPath, Status.UnauthorizedAccess));
                    }
                    catch (DirectoryNotFoundException)
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                            if (fileToCopy.OriginalPath == fileToCopy.NewPath)
                            {
                                File.Copy(fileToCopy.OriginalPath, targetPath);
                            }
                            else
                            {
                                if (fileToCopy.NewPath != targetPath)
                                {
                                    File.Copy(fileToCopy.NewPath, targetPath);
                                    File.Delete(fileToCopy.NewPath);
                                }
                            }
                            report.Add(new FileStatus(fileToCopy.OriginalPath, targetPath, Status.Success));
                        }
                        catch (UnauthorizedAccessException)
                        {
                            unauthorizedPath.Add(Path.GetDirectoryName(targetPath));
                            report.Add(new FileStatus(fileToCopy.OriginalPath, Status.UnauthorizedAccess));
                        }
                        catch
                        {
                            report.Add(new FileStatus(fileToCopy.OriginalPath, Status.GenericError));
                        }
                    }
                    catch (PathTooLongException)
                    {
                        report.Add(new FileStatus(fileToCopy.OriginalPath, Status.PathTooLong));
                    }
                    catch (FileNotFoundException)
                    {
                        report.Add(new FileStatus(fileToCopy.OriginalPath, Status.NotFound));
                    }
                    catch
                    {
                        report.Add(new FileStatus(fileToCopy.OriginalPath, Status.GenericError));
                    }
                }
                else
                {
                    unauthorizedPath.Add(Path.GetDirectoryName(targetPath));
                    report.Add(new FileStatus(fileToCopy.OriginalPath, Status.UnauthorizedAccess));
                }
            }
            return report;
        }

        public static List<string> ListAllowedFilesAndSubfolders(List<FileHistory> filesAndFolders, bool openFolder, bool openSubfolders)
        {
            allowedFiles.Clear();
            List<string> pathsNotAllowed = new List<string>();
            foreach (FileHistory fh in filesAndFolders)
            {
                if (Path.HasExtension(fh.NewPath))
                {
                    if (!allowedFiles.Contains(fh.NewPath))
                    {
                        string fileExtension = Path.GetExtension(fh.NewPath);
                        if ((fileExtension == ".docx") || (fileExtension == ".docm")
                            || (fileExtension == ".pptx") || (fileExtension == ".pptm")
                            || (fileExtension == ".xlsx") || (fileExtension == ".xlsm")
                            || (fileExtension == ".xps"))
                        {
                            allowedFiles.Add(fh.NewPath);
                        }
                    }
                    else
                    {
                        allowedFiles.Clear();
                        string fileExtension = Path.GetExtension(fh.NewPath);
                        if ((fileExtension == ".docx") || (fileExtension == ".docm")
                            || (fileExtension == ".pptx") || (fileExtension == ".pptm")
                            || (fileExtension == ".xlsx") || (fileExtension == ".xlsm")
                            || (fileExtension == ".xps"))
                        {
                            allowedFiles.Add(fh.NewPath);
                        }
                    }
                }
                else
                {
                    if (openFolder)
                    {
                        bool parentPathAllowedAccess = true;
                        foreach (string parentPathNotAllowed in pathsNotAllowed)
                        {
                            if ((parentPathNotAllowed.Length + 1) <= fh.NewPath.Length)
                                if ((parentPathNotAllowed + "\\") == fh.NewPath.Substring(0, parentPathNotAllowed.Length + 1))
                                    parentPathAllowedAccess = false;
                        }
                        if (parentPathAllowedAccess)
                        {
                            try
                            {
                                string[] filesInFolder = Directory.GetFiles(fh.NewPath);
                                ListAllowedFilesAndSubfolders(filesInFolder, openSubfolders, openSubfolders);
                                string[] foldersInFolder = Directory.GetDirectories(fh.NewPath);
                                ListAllowedFilesAndSubfolders(foldersInFolder, openSubfolders, openSubfolders);
                            }
                            catch
                            {
                                pathsNotAllowed.Add(fh.NewPath);
                            }
                        }
                    }
                }
            }
            return allowedFiles;
        }

        public static List<string> ListAllowedFilesAndSubfolders(string[] filesAndFolders, bool openFolder, bool openSubfolders)
        {
            List<string> pathsNotAllowed = new List<string>();
            foreach (string path in filesAndFolders)
            {
                if (Path.HasExtension(path))
                {
                    if (!allowedFiles.Contains(path))
                    {
                        string fileExtension = Path.GetExtension(path);
                        if ((fileExtension == ".docx") || (fileExtension == ".docm")
                            || (fileExtension == ".pptx") || (fileExtension == ".pptm")
                            || (fileExtension == ".xlsx") || (fileExtension == ".xlsm")
                            || (fileExtension == ".xps"))
                        {
                            allowedFiles.Add(path);
                        }
                    }
                    else
                    {
                        allowedFiles.Clear();
                        string fileExtension = Path.GetExtension(path);
                        if ((fileExtension == ".docx") || (fileExtension == ".docm")
                            || (fileExtension == ".pptx") || (fileExtension == ".pptm")
                            || (fileExtension == ".xlsx") || (fileExtension == ".xlsm")
                            || (fileExtension == ".xps"))
                        {
                            allowedFiles.Add(path);
                        }
                    }
                }
                else
                {
                    if (openFolder)
                    {
                        bool parentPathAllowedAccess = true;
                        foreach (string parentPathNotAllowed in pathsNotAllowed)
                        {
                            if ((parentPathNotAllowed.Length + 1) <= path.Length)
                                if ((parentPathNotAllowed + "\\") == path.Substring(0, parentPathNotAllowed.Length + 1))
                                    parentPathAllowedAccess = false;
                        }
                        if (parentPathAllowedAccess)
                        {
                            try
                            {
                                string[] filesInFolder = Directory.GetFiles(path);
                                ListAllowedFilesAndSubfolders(filesInFolder, openSubfolders, openSubfolders);
                                string[] foldersInFolder = Directory.GetDirectories(path);
                                ListAllowedFilesAndSubfolders(foldersInFolder, openSubfolders, openSubfolders);
                            }
                            catch
                            {
                                pathsNotAllowed.Add(path);
                            }
                        }
                    }
                }
            }
            return allowedFiles;
        }
        
        #endregion
    }
}
