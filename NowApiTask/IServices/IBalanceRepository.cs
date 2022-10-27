using NowApiTask.Model;

namespace NowApiTask.IServices
{
    public interface IBalanceRepository
    {

        public List<Balance> GetBalance();
    }
}
