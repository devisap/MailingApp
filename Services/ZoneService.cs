using System.ComponentModel.DataAnnotations;
using MailingApp.Dtos.Generals;
using MailingApp.Dtos.Requests;
using MailingApp.Dtos.Responses;
using MailingApp.Models.QueryBuilders;
using MailingApp.Utilities;

namespace MailingApp.Services
{
    public class ZoneService
    {
        private readonly ZoneQb _zoneQb;

        public ZoneService(ZoneQb zoneQb)
        {
            _zoneQb = zoneQb;
        }

        public ListData GetAllData(ReqGetListDto reqDto)
        {
            var listData = _zoneQb.GetListData(reqDto);
            return listData;
        }

        public ResZoneDto GetData(ReqGetDetailDto reqDto)
        {
            var data = _zoneQb.GetData(reqDto.id);
            return data;
        }

        public void InsertData(ReqCreateZoneDto reqDto, UserData userData)
        {
            _zoneQb.Insert(reqDto, userData);
        }

        public void UpdateData(ReqUpdateZoneDto reqDto, UserData userData)
        {
            _zoneQb.Update(reqDto, userData);
        }
        public void DeleteData(ReqGetDetailDto reqDto, UserData userData)
        {
            _zoneQb.UpdateStatus(reqDto.id, Const.STATUS_DATA_INACTIVE, userData);
        }

        public ResStatusFailedDto ValidateReqPostInsertData(ReqCreateZoneDto reqDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(reqDto);

            bool isInputFormat = Validator.TryValidateObject(reqDto, context, validationResults, true);
            if (!isInputFormat)
            {
                var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                return new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, errors[0], Const.HTTP_CODE_BAD_REQUEST);
            }

            return new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_BAD_REQUEST);
        }

        public ResStatusFailedDto ValidateReqPostUpdateData(ReqUpdateZoneDto reqDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(reqDto);

            bool isInputFormat = Validator.TryValidateObject(reqDto, context, validationResults, true);
            if (!isInputFormat)
            {
                var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                return new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, errors[0], Const.HTTP_CODE_BAD_REQUEST);
            }

            var inventoryMasterType = _zoneQb.GetData(reqDto.id);
            if (inventoryMasterType == null)
            {
                return new ResStatusFailedDto(Const.RESP_FAILED_REFERENCE, Const.RESP_FAILED_REFERENCE_DATA, Const.HTTP_CODE_BAD_REQUEST);
            }

            return new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_BAD_REQUEST);
        }

        public ResStatusFailedDto ValidateReqGetListData(ReqGetListDto reqDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(reqDto);

            bool isInputFormat = Validator.TryValidateObject(reqDto, context, validationResults, true);
            if (!isInputFormat)
            {
                var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                return new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, errors[0], Const.HTTP_CODE_BAD_REQUEST);
            }

            return new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_BAD_REQUEST);
        }

        public ResStatusFailedDto ValidateReqGetDetailData(ReqGetDetailDto reqDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(reqDto);

            bool isInputFormat = Validator.TryValidateObject(reqDto, context, validationResults, true);
            if (!isInputFormat)
            {
                var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                return new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, errors[0], Const.HTTP_CODE_BAD_REQUEST);
            }

            var inventoryClass = _zoneQb.GetData(reqDto.id);
            if (inventoryClass == null)
            {
                return new ResStatusFailedDto(Const.RESP_FAILED_REFERENCE, Const.RESP_FAILED_REFERENCE_DATA, Const.HTTP_CODE_BAD_REQUEST);
            }

            return new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_BAD_REQUEST);
        }
    }
}