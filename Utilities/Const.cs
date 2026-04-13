using MailingApp.Dtos.Generals;

namespace MailingApp.Utilities
{
    public static class Const
    {
        // JWT CONFIG
        public const int JWT_TOKEN_EXPIRE = 24;
        // END JWT CONFIG

        // STATUS DATA
        public const int STATUS_DATA_ACTIVE = 1;
        public const int STATUS_DATA_INACTIVE = 0;
        public const byte STATUS_NOTIFICAION_NOT_READ = 0;
        public const byte STATUS_NOTIFICAION_READ = 1;
        // END STATUS DATA

        // ROLE DATA
        public const string ROLE_ADMIN = "ADMIN";
        public const string ROLE_USER = "USER";
        public const string POLICY_ROLE_ADMIN = "AdminPolicy";
        public const string POLICY_ROLE_USER = "UserPolicy";
        // END ROLE DATA

        // GENERAL DATA
        public static DateTime CURR_DATETIME = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        // GENERAL DATA

        // HTTP CODE
        public const ushort HTTP_CODE_SUCCESS = 200;
        public const ushort HTTP_CODE_BAD_REQUEST = 400;
        public const ushort HTTP_CODE_UNAUTHORIZED = 401;
        public const ushort HTTP_CODE_FORBIDDEN = 403;
        public const ushort HTTP_CODE_INTERNAL_SERVER_ERROR = 500;
        // END HTTP 

        // RESPONSE STATUS
        public const string RES_SUCCESS = "success";
        public const string RES_FAILED = "failed";
        public const string RES_ERROR = "error";

        // APPROVAL STATUS
        public const byte APPROVAL_WAITING = 1;
        public const byte APPROVAL_APPROVE = 2;
        public const byte APPROVAL_REJECT = 3;
        // END APPROVAL STATUS

        // PATH
        public const string PATH_TEMPORARY_DATA = "wwwroot/temporaries";
        public const string RELATIVE_PATH_TEMPORARY_DATA = "temporaries";
        // END PATH

        public static readonly ResStatusDto RESP_ERROR = new ResStatusDto("error response", "something went wrong");
        public static readonly ResStatusDto RESP_SUCCESS_AUTHENTICATION = new ResStatusDto("success response", "success authentication");
        public static readonly ResStatusDto RESP_SUCCESS_RETRIEVE_DATA = new ResStatusDto("success response", "success retrieve data");
        public static readonly ResStatusDto RESP_SUCCESS_INSERT_DATA = new ResStatusDto("success response", "success insert data");
        public static readonly ResStatusDto RESP_SUCCESS_UPDATE_DATA = new ResStatusDto("success response", "success update data");
        public static readonly ResStatusDto RESP_SUCCESS_DESTROY_DATA = new ResStatusDto("success response", "success destroy data");
        public static readonly ResStatusDto RESP_SUCCESS_REVIVE_DATA = new ResStatusDto("success response", "success revive data");
        public static readonly ResStatusDto RESP_SUCCESS_REGISTRATION = new ResStatusDto("success response", "success registration");
        public static readonly ResStatusDto RESP_SUCCESS_UPLOAD_FILE = new ResStatusDto("success response", "success upload file");
        public static readonly ResStatusDto RESP_SUCCESS_GENERATE_FILE = new ResStatusDto("success response", "success generate file");

        public const string RESP_FAILED_MANDATORY = "failed request parameter mandatory";
        public const string RESP_FAILED_MANDATORY_JWT = "Access token is mandatory";
        public const string RESP_FAILED_MANDATORY_ID_DATA = "ID data is mandatory";
        public const string RESP_FAILED_MANDATORY_FILE = "File is mandatory";
        public const string RESP_FAILED_MANDATORY_PAGE_LIMIT = "Page limit is mandatory";
        public const string RESP_FAILED_MANDATORY_PAGE_NUMBER = "Page number is mandatory";
        public const string RESP_FAILED_MANDATORY_PERMIT_ID = "ID permit is mandatory";
        public const string RESP_FAILED_MANDATORY_ACTIVITY_TYPE = "Activity type is mandatory";
        public const string RESP_FAILED_MANDATORY_AREA_ID = "ID area is mandatory";
        public const string RESP_FAILED_MANDATORY_AREA_NAME = "Area name is mandatory";
        public const string RESP_FAILED_MANDATORY_LOCATION_ID = "ID location is mandatory";
        public const string RESP_FAILED_MANDATORY_LOCATION_NAME = "Location name is mandatory";
        public const string RESP_FAILED_MANDATORY_ZONE_NAME = "Zone name is mandatory";
        public const string RESP_REQPARAM_FORMAT_USER_EMAIL = "Email pengguna tidak memenuhi format untuk email address";
        public const string RESP_REQPARAM_FORMAT_USER_PASSWORD = "Password minimal 8 karakter";
        public const string RESP_REQPARAM_FORMAT_FILE_IMAGE = "File must be type of image like .jpg, .jpeg, .png";
        public const string RESP_REQPARAM_FORMAT_FILTER_DATE = "Format tanggal tidak valid";
        public const string RESP_FAILED_PERMISSION = "failed permission";
        public const string RESP_FAILED_PERMISSION_JWT_INVALID = "Token not valid or expired";
        public const string RESP_FAILED_PERMISSION_USER_NOT_VALID = "Email or password not found";
        public const string RESP_FAILED_PERMISSION_NOT_ALLOWED = "You don't have permission for this action";
        public const string RESP_FAILED_PERMISSION_APPROVAL_HAS_CONFIRMED = "This approval has been validated";
        public const string RESP_FAILED_PERMISSION_APPROVAL_NOT_COFIRMED = "This transaction has not been confirmed";
        public const string RESP_FAILED_PERMISSION_HAS_REVIEWED = "This transaction has been reviewed";
        public const string RESP_FAILED_REFERENCE = "failed reference data";
        public const string RESP_FAILED_REFERENCE_DATA = "Data not found";
        public const string RESP_FAILED_EXISTED = "failed data already exist";
        public const string RESP_FAILED_NOT_UPLOADED = "failed data not uploaded";
    }
}