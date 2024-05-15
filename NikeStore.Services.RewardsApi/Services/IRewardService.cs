using NikeStore.Services.RewardsApi.Message;

namespace NikeStore.Services.RewardsApi.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
