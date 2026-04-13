using MailingApp.Dtos.Generals;
using MailingApp.Dtos.Requests;
using MailingApp.Services;
using MailingApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MailingApp.Controllers
{
    [ApiController]
    [Route("api/area")]

    public class AreaController : ControllerBase
    {
        private readonly AreaService _areaService;
        private readonly PermissionUtil _permissionUtil;

        public AreaController(AreaService areaService, PermissionUtil permissionUtil)
        {
            _areaService = areaService;
            _permissionUtil = permissionUtil;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Get([FromQuery] ReqGetListDto reqDto)
        {
            try
            {
                // VALIDATION
                ResStatusFailedDto validation = _areaService.ValidateReqGetListData(reqDto);
                if (!validation.category.Equals(Const.RES_SUCCESS))
                {
                    return Ok(new ResFailedDto(new ResStatusDto(validation.category, validation.remark), validation.httpCode, null));
                }
                // END VALIDATION

                var areas = _areaService.GetAllData(reqDto);
                return Ok(new ResSuccessDto(Const.RESP_SUCCESS_RETRIEVE_DATA, areas));
            }
            catch (DbUpdateException dbEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database update error: " + dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Npgsql.NpgsqlException npgsqlEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database error: " + npgsqlEx.Message));
            }
            catch (Exception ex)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "An error occurred: " + ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("detail")]
        public IActionResult GetDetail([FromQuery] ReqGetDetailDto reqDto)
        {
            try
            {
                // VALIDATION
                ResStatusFailedDto validation = _areaService.ValidateReqGetDetailData(reqDto);
                if (!validation.category.Equals(Const.RES_SUCCESS))
                {
                    return Ok(new ResFailedDto(new ResStatusDto(validation.category, validation.remark), validation.httpCode, null));
                }
                // END VALIDATION

                var area = _areaService.GetData(reqDto);
                return Ok(new ResSuccessDto(Const.RESP_SUCCESS_RETRIEVE_DATA, area));
            }
            catch (DbUpdateException dbEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database update error: " + dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Npgsql.NpgsqlException npgsqlEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database error: " + npgsqlEx.Message));
            }
            catch (Exception ex)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "An error occurred: " + ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpPost]
        [Authorize(Policy = Const.POLICY_ROLE_ADMIN)]
        public ActionResult Store([FromBody] ReqCreateAreaDto reqDto)
        {
            try
            {
                // VALIDATION
                ResStatusFailedDto validation = _areaService.ValidateReqPostInsertData(reqDto);
                if (!validation.category.Equals(Const.RES_SUCCESS))
                {
                    return Ok(new ResFailedDto(new ResStatusDto(validation.category, validation.remark), validation.httpCode, null));
                }
                // END VALIDATION

                UserData userData = _permissionUtil.RetrieveUser();

                _areaService.InsertData(reqDto, userData);
                return Ok(new ResSuccessDto(Const.RESP_SUCCESS_INSERT_DATA, null));
            }
            catch (DbUpdateException dbEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database update error: " + dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Npgsql.NpgsqlException npgsqlEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database error: " + npgsqlEx.Message));
            }
            catch (Exception ex)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "An error occurred: " + ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpPut]
        [Authorize(Policy = Const.POLICY_ROLE_ADMIN)]
        public ActionResult Update(ReqUpdateAreaDto reqDto)
        {
            try
            {
                // VALIDATION
                ResStatusFailedDto validation = _areaService.ValidateReqPostUpdateData(reqDto);
                if (!validation.category.Equals(Const.RES_SUCCESS))
                {
                    return Ok(new ResFailedDto(new ResStatusDto(validation.category, validation.remark), validation.httpCode, null));
                }
                // END VALIDATION
                UserData userData = _permissionUtil.RetrieveUser();

                _areaService.UpdateData(reqDto, userData);
                return Ok(new ResSuccessDto(Const.RESP_SUCCESS_UPDATE_DATA, null));
            }
            catch (DbUpdateException dbEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database update error: " + dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Npgsql.NpgsqlException npgsqlEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database error: " + npgsqlEx.Message));
            }
            catch (Exception ex)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "An error occurred: " + ex.InnerException?.Message ?? ex.Message));
            }
        }

        [HttpDelete]
        [Authorize(Policy = Const.POLICY_ROLE_ADMIN)]
        public ActionResult Delete([FromQuery] ReqGetDetailDto reqDto)
        {
            try
            {
                // VALIDATION
                ResStatusFailedDto validation = _areaService.ValidateReqGetDetailData(reqDto);
                if (!validation.category.Equals(Const.RES_SUCCESS))
                {
                    return Ok(new ResFailedDto(new ResStatusDto(validation.category, validation.remark), validation.httpCode, null));
                }
                // END VALIDATION
                UserData userData = _permissionUtil.RetrieveUser();

                _areaService.DeleteData(reqDto, userData);
                return Ok(new ResSuccessDto(Const.RESP_SUCCESS_DESTROY_DATA, null));
            }
            catch (DbUpdateException dbEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database update error: " + dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Npgsql.NpgsqlException npgsqlEx)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "Database error: " + npgsqlEx.Message));
            }
            catch (Exception ex)
            {
                return Ok(new ResErrorDto(Const.RESP_ERROR, "An error occurred: " + ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}