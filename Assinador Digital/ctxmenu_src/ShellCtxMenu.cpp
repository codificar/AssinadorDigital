#include "stdafx.h"
#include "resource.h"
#include "priv.h"
#include "ShellExt.h"
#include "CtxMenu.h"

#include <shlobj.h>
#include <shlguid.h>
#include "io.h"
#include <iostream> 
#include <fstream>
#include <string>

// utilities
#include "ShUtils.h"
#include "FileProcess.h"
#include "CancelDlg.h"   

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

using namespace std;



/*----------------------------------------------------------------

FUNCTION: CShellExt::InvokeCommand(LPCMINVOKECOMMANDINFO)

PURPOSE: Called by the shell after the user has selected on of the
menu items that was added in QueryContextMenu(). This is 
the function where you get to do your real work!!!

PARAMETERS:
lpcmi - Pointer to an CMINVOKECOMMANDINFO structure

RETURN VALUE:


COMMENTS:

----------------------------------------------------------------*/

STDMETHODIMP CShellExt::InvokeCommand(LPCMINVOKECOMMANDINFO lpcmi)
{
    // look at all the MFC stuff in here! call this to allow us to 
    // not blow up when we try to use it.
    AFX_MANAGE_STATE(AfxGetStaticModuleState( ));

    HINSTANCE hInst = AfxGetInstanceHandle();

    ODS("CShellExt::InvokeCommand()\r\n");

    HWND hParentWnd = lpcmi->hwnd;

    HRESULT hr = NOERROR;

    //If HIWORD(lpcmi->lpVerb) then we have been called programmatically
    //and lpVerb is a command that should be invoked.  Otherwise, the shell
    //has called us, and LOWORD(lpcmi->lpVerb) is the menu ID the user has
    //selected.  Actually, it's (menu ID - idCmdFirst) from QueryContextMenu().
    UINT idCmd = 0;

    if (!HIWORD(lpcmi->lpVerb)) 
    {
        idCmd = LOWORD(lpcmi->lpVerb);

        // process it
        switch (idCmd) 
        {
        case 0: 
            {
                // get a CWnd for Explorer
                CWnd *pParentWnd = NULL;
                if (hParentWnd!=NULL)
                    pParentWnd = CWnd::FromHandle(hParentWnd);
                // disable explorer
                ::PostMessage(hParentWnd, WM_ENABLE, (WPARAM)FALSE, (LPARAM)0);

                try
                {
                    int iFiles = m_csaPaths.GetSize();
                    string arquivos ="";
                    for (int i=0; i < iFiles; i++) 
                    {
                        string csFile = (string) (LPCTSTR)m_csaPaths.GetAt(i);
                        if ( (iFiles-1)!=i)
                            arquivos=arquivos+csFile+"|";
                        else
                            arquivos=arquivos+csFile;
                    }

                    //Find where are my dll
                    // to find out where to look for 
                    // the digital signer executable
                    char   FileName[MAX_PATH];
                    GetModuleFileNameA(AfxGetInstanceHandle(), FileName, MAX_PATH);
                    CString RtnVal=FileName;
                    RtnVal = RtnVal.Left(RtnVal.ReverseFind('\\'));
                    string exec=(string) (LPCTSTR)RtnVal;
                    //Creates the commandline
                    exec=exec+"\\Assinadordigital.exe /a \""+arquivos+"\"";

                    PROCESS_INFORMATION pi;
                    STARTUPINFO si;
                    memset(&si,0,sizeof(si));
                    si.cb= sizeof(si);


                    if (!CreateProcessA(NULL,(LPSTR)exec.c_str(),NULL,NULL,false,NULL,NULL,NULL,&si,&pi)){
                        LPVOID lpMsgBuf;
                        FormatMessage( 
                            FORMAT_MESSAGE_ALLOCATE_BUFFER | 
                            FORMAT_MESSAGE_FROM_SYSTEM | 
                            FORMAT_MESSAGE_IGNORE_INSERTS,
                            NULL,
                            GetLastError(),
                            MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
                            (LPTSTR) &lpMsgBuf,
                            0,
                            NULL 
                            );
                        MessageBox(NULL,(LPCTSTR)lpMsgBuf,"Erro",MB_ICONERROR|MB_OK);
                        LocalFree( lpMsgBuf );
                    }
					//HANDLE dsHandle =OpenProcess(PROCESS_ALL_ACCESS,false,pi.dwProcessId);
					

                }

                catch (CException *e) 
                {
                    e->ReportError();
                    e->Delete();
                }

                //reenable Explorer
                ::PostMessage(hParentWnd, WM_ENABLE, (WPARAM)TRUE, (LPARAM)0);

                break;
            }
        case 1:
            {
                // get a CWnd for Explorer
                CWnd *pParentWnd = NULL;
                if (hParentWnd!=NULL)
                    pParentWnd = CWnd::FromHandle(hParentWnd);
                // disable explorer
                ::PostMessage(hParentWnd, WM_ENABLE, (WPARAM)FALSE, (LPARAM)0);

                try
                {
                    int iFiles = m_csaPaths.GetSize();
                    string arquivos ="";
                    for (int i=0; i < iFiles; i++) 
                    {
                        string csFile = (string) (LPCTSTR)m_csaPaths.GetAt(i);
                        if ( (iFiles-1)!=i)
                            arquivos=arquivos+csFile+"|";
                        else
                            arquivos=arquivos+csFile;
                    }

                    //Find where are my dll
                    // to find out where to look for 
                    // the digital signer executable
                    char   FileName[MAX_PATH];
                    GetModuleFileNameA(AfxGetInstanceHandle(), FileName, MAX_PATH);
                    CString RtnVal=FileName;
                    RtnVal = RtnVal.Left(RtnVal.ReverseFind('\\'));
                    string exec=(string) (LPCTSTR)RtnVal;
                    //Creates the commandline
                    exec=exec+"\\Assinadordigital.exe /r \""+arquivos+"\"";

                    PROCESS_INFORMATION pi;
                    STARTUPINFO si;
                    memset(&si,0,sizeof(si));
                    si.cb= sizeof(si);

                    if (!CreateProcessA(NULL,(LPSTR)exec.c_str(),NULL,NULL,false,NULL,NULL,NULL,&si,&pi)){
                        LPVOID lpMsgBuf;
                        FormatMessage( 
                            FORMAT_MESSAGE_ALLOCATE_BUFFER | 
                            FORMAT_MESSAGE_FROM_SYSTEM | 
                            FORMAT_MESSAGE_IGNORE_INSERTS,
                            NULL,
                            GetLastError(),
                            MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
                            (LPTSTR) &lpMsgBuf,
                            0,
                            NULL 
                            );
                        MessageBox(NULL,(LPCTSTR)lpMsgBuf,"Erro",MB_ICONERROR|MB_OK);
                        LocalFree( lpMsgBuf );
                    }
                    //DWORD erro = GetLastError();

                }

                catch (CException *e) 
                {
                    e->ReportError();
                    e->Delete();
                }

                //reenable Explorer
                ::PostMessage(hParentWnd, WM_ENABLE, (WPARAM)TRUE, (LPARAM)0);
            }
            break;
        case 2:{
            // get a CWnd for Explorer
            CWnd *pParentWnd = NULL;
            if (hParentWnd!=NULL)
                pParentWnd = CWnd::FromHandle(hParentWnd);
            // disable explorer
            ::PostMessage(hParentWnd, WM_ENABLE, (WPARAM)FALSE, (LPARAM)0);

            try
            {
                int iFiles = m_csaPaths.GetSize();
                string arquivos ="";
                for (int i=0; i < iFiles; i++) 
                {
                    string csFile = (string) (LPCTSTR)m_csaPaths.GetAt(i);
                    if ( (iFiles-1)!=i)
                        arquivos=arquivos+csFile+"|";
                    else
                        arquivos=arquivos+csFile;
                }

                //Find where are my dll
                // to find out where to look for 
                // the digital signer executable
                char   FileName[MAX_PATH];
                GetModuleFileNameA(AfxGetInstanceHandle(), FileName, MAX_PATH);
                CString RtnVal=FileName;
                RtnVal = RtnVal.Left(RtnVal.ReverseFind('\\'));
                string exec=(string) (LPCTSTR)RtnVal;
                //Creates the commandline
                exec=exec+"\\Assinadordigital.exe /v \""+arquivos+"\"";

                PROCESS_INFORMATION pi;
                STARTUPINFO si;
                memset(&si,0,sizeof(si));
                si.cb= sizeof(si);

                if (!CreateProcessA(NULL,(LPSTR)exec.c_str(),NULL,NULL,false,NULL,NULL,NULL,&si,&pi)){
                    LPVOID lpMsgBuf;
                    FormatMessage( 
                        FORMAT_MESSAGE_ALLOCATE_BUFFER | 
                        FORMAT_MESSAGE_FROM_SYSTEM | 
                        FORMAT_MESSAGE_IGNORE_INSERTS,
                        NULL,
                        GetLastError(),
                        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
                        (LPTSTR) &lpMsgBuf,
                        0,
                        NULL 
                        );
                    MessageBox(NULL,(LPCTSTR)lpMsgBuf,"Erro",MB_ICONERROR|MB_OK);
                    LocalFree( lpMsgBuf );
                }
                //DWORD erro = GetLastError();

            }

            catch (CException *e) 
            {
                e->ReportError();
                e->Delete();
            }

            //reenable Explorer
            ::PostMessage(hParentWnd, WM_ENABLE, (WPARAM)TRUE, (LPARAM)0);
               }break;
        }	// switch on command
    }

    return hr;
}


////////////////////////////////////////////////////////////////////////
//
//  FUNCTION: CShellExt::GetCommandString(...)
//
//  PURPOSE: Retrieve various text strinsg associated with the context menu
//
//	Param			Type			Use
//	-----			----			---
//	idCmd			UINT			ID of the command
//	uFlags			UINT			which type of info are we requesting
//	reserved		UINT *			must be NULL
//	pszName			LPSTR			output buffer
//	cchMax			UINT			max chars to copy to pszName
//
////////////////////////////////////////////////////////////////////////

STDMETHODIMP CShellExt::GetCommandString(UINT idCmd,
                                         UINT uFlags,
                                         UINT FAR *reserved,
                                         LPSTR pszName,
                                         UINT cchMax)
/*STDMETHODIMP CShellExt::GetCommandString(UINT_PTR idCmd,
                                         UINT uFlags,
                                         UINT FAR *reserved,
                                         LPSTR pszName,
                                         UINT cchMax)*/
{
    ODS("CShellExt::GetCommandString()\r\n");

    AFX_MANAGE_STATE(AfxGetStaticModuleState( ));

    HINSTANCE hInst = AfxGetInstanceHandle();

    switch (uFlags) 
    {
    case GCS_HELPTEXT:		// fetch help text for display at the bottom of the 
        // explorer window
        switch (idCmd)
        {
        case 0:
            strncpy(pszName, "Assinar...", cchMax);
            break;
        case 1:
            strncpy(pszName, "Remover Assinatura(s)", cchMax);
            break;
		case 2:
			strncpy(pszName, "Visualizar Assinatura(s)", cchMax);
            break;
        default:
            strncpy(pszName, SHELLEXNAME, cchMax);
            break;
        }
        break;

    case GCS_VALIDATE:
        break;

    case GCS_VERB:			// language-independent command name for the menu item 
        switch (idCmd)
        {
        case 0:
            strncpy(pszName, "Assinar...", cchMax);
            break;
        case 1:
            strncpy(pszName, "Remover Assinatura(s)...", cchMax);
            break;
		case 2:
			strncpy(pszName, "Visualizar Assinatura(s)", cchMax);
            break;
        default:
            strncpy(pszName, SHELLEXNAME, cchMax);
            break;
        }

        break;
    }
    return NOERROR;
}

///////////////////////////////////////////////////////////////////////////
//
//  FUNCTION: CShellExt::QueryContextMenu(HMENU, UINT, UINT, UINT, UINT)
//
//  PURPOSE: Called by the shell just before the context menu is displayed.
//           This is where you add your specific menu items.
//
//  PARAMETERS:
//    hMenu      - Handle to the context menu
//    indexMenu  - Index of where to begin inserting menu items
//    idCmdFirst - Lowest value for new menu ID's
//    idCmtLast  - Highest value for new menu ID's
//    uFlags     - Specifies the context of the menu event
//
//  RETURN VALUE:
//
//
//  COMMENTS:
//
///////////////////////////////////////////////////////////////////////////

STDMETHODIMP CShellExt::QueryContextMenu(HMENU hMenu,
                                         UINT indexMenu,
                                         UINT idCmdFirst,
                                         UINT idCmdLast,
                                         UINT uFlags)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState( ));

    //return CreateShellExtMenu(hMenu, 
    //                           indexMenu, 
    //                           idCmdFirst, 
    //                           idCmdLast, 
    //                           uFlags, 
    //                           (HBITMAP)m_menuBmp.GetSafeHandle());
    HBITMAP imagens[4]={(HBITMAP)m_menuBmp.GetSafeHandle(),
        (HBITMAP)m_submenuBmp1.GetSafeHandle(),
        (HBITMAP)m_submenuBmp2.GetSafeHandle(),
        (HBITMAP)m_submenuBmp3.GetSafeHandle()};

    return CreateShellExtMenuBmpArray(hMenu, 
        indexMenu, 
        idCmdFirst, 
        idCmdLast, 
        uFlags, 
        imagens
        );

}

