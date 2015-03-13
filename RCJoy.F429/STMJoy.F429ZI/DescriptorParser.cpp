#include "def.h"
#include "DescriptorParser.h"


static u8 __CollectionCounter = 0;

static u16 TotalBits;
static u8 MultiReport; 

static u16 __Bits;
static u16 __Usages;
static u16 __Count;
static u16 __CurrentUsageBlock;
static u8 __EOF = false;
static u8 __ReportID = 0;


static void CreateCollecedInput()
{
	TotalBits += __Bits * __Count;
}

static void ParseLocal(u8 tag, u32 val)
{
	switch (tag)
	{
	case TAG_LOCAL_USAGE:
		__Usages++;
		break;

	case TAG_LOCAL_USAGEMIN:
	case TAG_LOCAL_USAGEMAX:
		break;
	}
}


static void ParseGlobal(u8 tag, u32 val)
{
	switch (tag)
	{
	case TAG_GLOBAL_USAGEPAGE:
		__CurrentUsageBlock = val;
		__Usages = 0;
		break;


	case TAG_GLOBAL_LOGICALMIN:
	case TAG_GLOBAL_LOGICALMAX:
	case TAG_GLOBAL_PHYSMIN:
	case TAG_GLOBAL_PHYSMAX:
	case TAG_GLOBAL_UNITEXP:
	case TAG_GLOBAL_UNIT:
		break;

	case TAG_GLOBAL_REPORTSIZE:
		__Bits = val;
		break;

	case TAG_GLOBAL_REPORTID:
		__ReportID = val;
		MultiReport = true;
		break;


	case TAG_GLOBAL_REPORTCOUNT:
		__Count = val;
		break;
	}
}

static void ParseMain(u8 tag, u8 val)
{
	switch (tag)
	{
	case TAG_MAIN_COLLECTION:
		__CollectionCounter++;
		__Usages = 0;
		break;

	case TAG_MAIN_ENDCOLLECTION:
		__CollectionCounter--;
		__EOF = __CollectionCounter <= 0;
		__Usages = 0;
		break;

	case TAG_MAIN_INPUT:
		if (__ReportID == 0 || __ReportID == 1)
			CreateCollecedInput();

		break;

	case TAG_MAIN_OUTPUT:
	case TAG_MAIN_FEATURE:
		__Bits = 0;
		__Count = 0;
		__Usages = 0;
		break;
	}
}

static void ParseCommand(u8 _type, u8 _tag, u32 _val)
{
	switch (_type)
	{
		case TYPE_MAIN:
			ParseMain(_tag, _val);
			break;

		case TYPE_GLOBAL:
			ParseGlobal(_tag, _val);
			break;

		case TYPE_LOCAL:
			 ParseLocal(_tag, _val);
			 break;

		default:
			__EOF = true;
			break;
	}
}

void ParseReportDescriptor(uint8_t *report, uint16_t len, uint16_t* bitlen, uint8_t *mreport)
{
	u16 i = 0;
	TotalBits = 0;
	MultiReport = 0;
	__EOF = false;

	while (i<len && !__EOF)
	{
		u8 tag = *(report + i);

		u8 __ItemType = tag & TYPE_MASK;
		u8 __ItemTag = tag & TAG_MASK;
		u8 __ItemSize = tag & DATA_SIZE_MASK;

		u32 __ItemValue = 0;

		i++;

		for (int j = 0; j < __ItemSize; j++, i++)
		{
			if (i<=len)
				__ItemValue = (__ItemValue << 8) | *(report+i);
		}

		ParseCommand(__ItemType, __ItemTag, __ItemValue);
	}

	*bitlen = TotalBits;
	*mreport = MultiReport;
}