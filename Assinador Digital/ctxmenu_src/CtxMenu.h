// CtxMenu.h : main header file for the CTXMENU DLL
//

#if !defined(AFX_CTXMENU_H__982905C7_3928_11D3_A09E_00500402F30B__INCLUDED_)
#define AFX_CTXMENU_H__982905C7_3928_11D3_A09E_00500402F30B__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CCtxMenuApp
// See CtxMenu.cpp for the implementation of this class
//

class CCtxMenuApp : public CWinApp
{
public:
	CCtxMenuApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCtxMenuApp)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL

	//{{AFX_MSG(CCtxMenuApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

extern UINT      g_cRefThisDll;

/////////////////////////////////////////////////////////////
// use GUIDGEN to generate a GUID for your shell extension
// ...
//use GUIDGEN to generate a GUID for your shell extension. call
//it "CLSID_MyFileCtxMenuID"
//ex. (don't use this GUID!!)
//DEFINE_GUID(CLSID_MyFileCtxMenuID, 
//0xc14f7681, 0x33d8, 0x11d3, 0xa0, 0x9b, 0x0, 0x50, 0x4, 0x2, 0xf3, 0xb);
// {DBD1658A-50E2-48b4-8C6B-9C9F2342904C}
DEFINE_GUID(CLSID_MyFileCtxMenuID, 
0xdbd1658a, 0x50e2, 0x48b4, 0x8c, 0x6b, 0x9c, 0x9f, 0x23, 0x42, 0x90, 0x4c);



/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CTXMENU_H__982905C7_3928_11D3_A09E_00500402F30B__INCLUDED_)
