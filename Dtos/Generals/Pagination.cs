namespace MailingApp.Dtos.Generals
{
    public record Pagination(int pageCurrent, int pageLast, int pageLimit, int totalFiltered, int totalData);
}