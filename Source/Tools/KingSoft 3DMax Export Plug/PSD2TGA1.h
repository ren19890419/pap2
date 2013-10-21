// PSD2TGA1.h: interface for the CPSD2TGA class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_PSD2TGA1_H__D1B9813B_C890_4B55_8E40_152ED2BE7241__INCLUDED_)
#define AFX_PSD2TGA1_H__D1B9813B_C890_4B55_8E40_152ED2BE7241__INCLUDED_

#include "windows.h"
#include "stdio.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CPSD2TGA  
{
public:
	CPSD2TGA();
	virtual ~CPSD2TGA();

	HRESULT PSD2TGA(LPSTR pPSDFileNane,LPSTR pTGAFileName);
private:
	
#pragma pack(push,1)
	struct _TGAHeader 
	{
		unsigned char   Id ;
		unsigned char   ColourMapType ;
		unsigned char   ImageType ;
		unsigned short  ColourMapOrigin ;
		unsigned short  ColourMapLength ;
		unsigned char   ColourMapDepth ;
		unsigned short  x_origin ;
		unsigned short  y_origin ;
		unsigned short  width ;
		unsigned short  height ;
		unsigned char   bitsperpixel ;
		unsigned char   imagedescriptor ;
	};
#pragma pack(pop)

	void WriteChannel(BYTE Data, int Count, BYTE *Start, int Step);
	void Reverse(BYTE *data, int, int);
	HRESULT Save2Tga(LPSTR strFilename,
						   DWORD dwWidth,     // dwWidth��dwHeightΪ���ߣ�
						   DWORD dwHeight,
						   BYTE* pixels      // pixelsΪ���Ŀ��ָ�룬
						   );

	DWORD Read32(FILE* pFile);
	WORD Read16(FILE* pFile);
	HRESULT UnPackPSD(FILE *fp,          // fpΪPSD�ļ�ָ�룬
					  DWORD dwWidth,     // dwWidth��dwHeightΪ���ߣ�
					  DWORD dwHeight,
					  BYTE* pixels,      // pixelsΪ���Ŀ��ָ�룬
					  WORD ChannelCnt,   // ChannelCntΪͨ����
					  WORD Compression );

private:
};

#endif // !defined(AFX_PSD2TGA1_H__D1B9813B_C890_4B55_8E40_152ED2BE7241__INCLUDED_)