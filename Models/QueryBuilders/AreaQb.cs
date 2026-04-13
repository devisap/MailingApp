using MailingApp.Datas;
using MailingApp.Dtos.Generals;
using MailingApp.Dtos.Requests;
using MailingApp.Dtos.Responses;
using MailingApp.Models.Entites;
using MailingApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MailingApp.Models.QueryBuilders
{
    public class AreaQb
    {
        private readonly AppDbContext _dbContext;

        public AreaQb(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ListData GetListData(ReqGetListDto reqDto)
        {
            var datas =
            (
                from are in _dbContext.Areas
                where are.are_is_active == Const.STATUS_DATA_ACTIVE
                join z in _dbContext.Zones
                on are.z_id equals z.z_id into ZoneGroup
                from z in ZoneGroup.DefaultIfEmpty()
                select new ResAreaDto
                {
                    areId = are.are_id,
                    areName = are.are_name,
                    zId = z.z_id,
                    zName = z.z_name,
                    areIsActive = are.are_is_active,
                    createdAt = are.created_at
                }
            );

            int totalData = datas.Count();

            // FILTER DATA
            if (!string.IsNullOrEmpty(reqDto.search))
            {
                datas = datas.Where(x =>
                    EF.Functions.ILike(x.zName, $"%{reqDto.search}%")
                    || EF.Functions.ILike(x.areName, $"%{reqDto.search}%")
                );
            }

            if (!string.IsNullOrEmpty(reqDto.orderBy))
            {
                string ordering = reqDto.orderBy + (reqDto.ordering == "desc" ? " descending" : " ascending");
                datas = datas.OrderBy(ordering);
            }
            else
            {
                datas = datas.OrderByDescending(x => x.createdAt);
            }

            List<ResAreaDto> listData;
            Pagination pagination;

            if (reqDto.pageLimit != -1)
            {
                // CONF PAGINATION
                int totalFiltered = datas.Count();
                int totalPage = (int)Math.Ceiling((double)totalFiltered / (double)reqDto.pageLimit);
                pagination = new Pagination(reqDto.pageNumber, totalPage, reqDto.pageLimit, totalFiltered, totalData);
                // END CONF PAGINATION

                int offset = (reqDto.pageNumber - 1) * reqDto.pageLimit;
                listData = datas.Skip(offset).Take(reqDto.pageLimit).ToList();
            }
            else
            {
                // CONF PAGINATION
                int totalFiltered = datas.Count();
                int totalPage = 1;
                pagination = new Pagination(reqDto.pageNumber, totalPage, reqDto.pageLimit, totalFiltered, totalData);
                // END CONF PAGINATION

                listData = datas.ToList();
            }

            return new ListData(listData, pagination);
            // END FILTER DATA
        }

        public ResAreaDto GetData(int id)
        {
            var data =
            (
                from are in _dbContext.Areas
                where are.are_id == id && are.are_is_active == Const.STATUS_DATA_ACTIVE
                join z in _dbContext.Zones
                on are.z_id equals z.z_id into ZoneGroup
                from z in ZoneGroup.DefaultIfEmpty()
                select new ResAreaDto
                {
                    areId = are.are_id,
                    areName = are.are_name,
                    zId = z.z_id,
                    zName = z.z_name,
                    areIsActive = are.are_is_active,
                    createdAt = are.created_at
                }
            ).FirstOrDefault();

            return data;
        }

        public int Insert(ReqCreateAreaDto reqDto, UserData userData)
        {
            DateTime currDate = DateFormatUtil.GetCurrentDate();
            var area = new Area()
            {
                are_name = reqDto.areName,
                z_id = reqDto.zId,
                are_is_active = Const.STATUS_DATA_ACTIVE,
                created_at = currDate,
                created_by = userData.uId,
                updated_at = currDate,
                updated_by = userData.uId
            };

            _dbContext.Areas.Add(area);
            _dbContext.SaveChanges();
            return area.are_id;
        }

        public bool Update(ReqUpdateAreaDto reqDto, UserData userData)
        {
            DateTime currDate = DateFormatUtil.GetCurrentDate();

            var data = _dbContext.Areas.Find(reqDto.id);
            data.z_id = reqDto.zId;
            data.are_name = reqDto.areName;
            data.updated_at = currDate;
            data.updated_by = userData.uId;
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateStatus(int id, byte statusNew, UserData userData)
        {
            DateTime currDate = DateFormatUtil.GetCurrentDate();

            var data = _dbContext.Areas.Find(id);
            data.are_is_active = statusNew;
            data.updated_at = currDate;
            data.updated_by = userData.uId;

            if (statusNew == Const.STATUS_DATA_INACTIVE)
            {
                data.deleted_at = currDate;
                data.deleted_by = userData.uId;
            }
            else
            {
                data.deleted_at = null;
                data.deleted_by = null;
            }

            _dbContext.SaveChanges();

            return true;
        }
    }
}