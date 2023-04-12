using System;
using System.Collections.Generic;
using System.Text;

namespace MControl.Alerts
{
    public enum ExceptionType
    {
        NonSpecified = 0,
        UnExcpectedError = 1000,
        XmlParsingError = 1001,
        XmlValidatingFailed = 1002,
        UnsupportParameter = 1003,
        InvalidApplicationContent = 1004,
        ApplicationError = 1005,
        InvalidCastException = 1006,
        ArgumentException = 1007,
        FormatException = 1008,
        ConfigFileException = 1009,
        DBConnectionError = 1110,
        DBInsertError = 1111,
        DBUpdateError = 1112,
        HttpException = 1113,
        SmppException = 1114,
        TimeOutException = 1115,
        DllReturnError = 1116,
        CarrierNotResponse = 1117,
        CarrierException = 1118,
        InvalidIAppResult = 1119,
        NetworkConnectionError = 1120
    }

    public enum AlertPriority
    {
        Normal = 0,
        Warnning = 1,
        Error = 2
    }

    public enum AlertTypes
    {
        None = 0,
        Mail = 1,
        Sms = 2,
        MailAndSms = 3,
        Site = 4,
        SiteAndMail = 5,
        AllAlarms = 6
    }
}
