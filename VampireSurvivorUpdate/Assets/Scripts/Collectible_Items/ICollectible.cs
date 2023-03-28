namespace Collectible_Items
{
    public interface ICollectible
    {
        /// <summary>
        /// Will trigger the corresponding Collect function for every ICollectible children
        /// </summary>
        public void Collect();
    }
}
