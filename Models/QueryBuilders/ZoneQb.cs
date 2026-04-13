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
    public class ZoneQb
    {
        private readonly AppDbContext _dbContext;

        public ZoneQb(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ListData GetListData(ReqGetListDto reqDto)
        {
            var datas = _dbContext.Zones.Select(x => new ResZoneDto
            {
                zId = x.z_id,
                zName = x.z_name,
                zIsActive = x.z_is_active,
                createdAt = x.created_at
            });

            datas = datas.Where(x => x.zIsActive == Const.STATUS_DATA_ACTIVE);
            int totalData = datas.Count();

            // FILTER DATA
            if (!string.IsNullOrEmpty(reqDto.search))
            {
                datas = datas.Where(x =>
                    EF.Functions.ILike(x.zName, $"%{reqDto.search}%")
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

            List<ResZoneDto> listData;
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

        public ResZoneDto GetData(int id)
        {
            var data = _dbContext.Zones.Where(x => x.z_id == id)
                .Select(x => new ResZoneDto
                {
                    zId = x.z_id,
                    zName = x.z_name,
                    zIsActive = x.z_is_active,
                    createdAt = x.created_at
                }).FirstOrDefault();

            return data;
        }

        public int Insert(ReqCreateZoneDto reqDto, UserData userData)
        {
            DateTime currDate = DateFormatUtil.GetCurrentDate();
            var zone = new Zone()
            {
                z_name = reqDto.zName,
                z_is_active = Const.STATUS_DATA_ACTIVE,
                created_at = currDate,
                created_by = userData.uId,
                updated_at = currDate,
                updated_by = userData.uId
            };

            _dbContext.Zones.Add(zone);
            _dbContext.SaveChanges();
            return zone.z_id;
        }

        public bool Update(ReqUpdateZoneDto reqDto, UserData userData)
        {
            DateTime currDate = DateFormatUtil.GetCurrentDate();

            var data = _dbContext.Zones.Find(reqDto.id);
            data.z_name = reqDto.zName;
            data.updated_at = currDate;
            data.updated_by = userData.uId;
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateStatus(int id, byte statusNew, UserData userData)
        {
            DateTime currDate = DateFormatUtil.GetCurrentDate();

            var data = _dbContext.Zones.Find(id);
            data.z_is_active = statusNew;
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