using System.ComponentModel.DataAnnotations;
using MailingApp.Dtos.Generals;
using MailingApp.Dtos.Requests;
using MailingApp.Dtos.Responses;
using MailingApp.Models.QueryBuilders;
using MailingApp.Utilities;

namespace MailingApp.Services
{
    public class AreaService
    {
        private readonly AreaQb _areaQb;
        private readonly ZoneQb _zoneQb;

        public AreaService(AreaQb areaQb, ZoneQb zoneQb)
        {
            _areaQb = areaQb;
            _zoneQb = zoneQb;
        }

        public ListData GetAllData(ReqGetListDto reqDto)
        {
            var listData = _areaQb.GetListData(reqDto);
            return listData;
        }

        public ResAreaDto GetData(ReqGetDetailDto reqDto)
        {
            var data = _areaQb.GetData(reqDto.id);
            return data;
        }

        public void InsertData(ReqCreateAreaDto reqDto, UserData userData)
        {
            _areaQb.Insert(reqDto, userData);
        }

        public void UpdateData(ReqUpdateAreaDto reqDto, UserData userData)
        {
            _areaQb.Update(reqDto, userData);
        }
        public void DeleteData(ReqGetDetailDto reqDto, UserData userData)
        {
            _areaQb.UpdateStatus(reqDto.id, Const.STATUS_DATA_INACTIVE, userData);
        }

        public ResStatusFailedDto ValidateReqPostInsertData(ReqCreateAreaDto reqDto)
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

        public ResStatusFailedDto ValidateReqPostUpdateData(ReqUpdateAreaDto reqDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(reqDto);

            bool isInputFormat = Validator.TryValidateObject(reqDto, context, validationResults, true);
            if (!isInputFormat)
            {
                var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                return new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, errors[0], Const.HTTP_CODE_BAD_REQUEST);
            }

            var zone = _zoneQb.GetData(reqDto.id);
            if (zone == null)
            {
                return new ResStatusFailedDto(Const.RESP_FAILED_REFERENCE, Const.RESP_FAILED_REFERENCE_DATA_ZONE, Const.HTTP_CODE_BAD_REQUEST);
            }

            var area = _areaQb.GetData(reqDto.id);
            if (area == null)
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

            var area = _areaQb.GetData(reqDto.id);
            if (area == null)
            {
                return new ResStatusFailedDto(Const.RESP_FAILED_REFERENCE, Const.RESP_FAILED_REFERENCE_DATA, Const.HTTP_CODE_BAD_REQUEST);
            }

            return new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_BAD_REQUEST);
        }
    }
}