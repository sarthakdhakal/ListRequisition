using ListData.Models;

namespace ListData.Services
{
    public interface IRequestionService
    {
        public List<InitialReturn> GetInitials(List<RequistionApproved> values);

        public List<ReturnList> ReturnLists( List<RequistionApproved> values);
} }
