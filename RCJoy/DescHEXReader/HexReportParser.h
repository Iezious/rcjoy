
#include <max3421e.h>
#include <usbhost.h>
#include <usb_ch9.h>
#include <Usb.h>
#include <usbhub.h>
#include <hid.h>
#include <printhex.h>
#include <hidescriptorparser.h>

class HexReportParser : public USBReadParser
{
  public :
    void Parse(uint16_t, const uint8_t*, const uint16_t&);
};
