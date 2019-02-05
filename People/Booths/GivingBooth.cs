using System;
using BoothItems;

namespace People
{
    /// <summary>
    /// The class representing a giving booth.
    /// </summary>
    [Serializable]
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Initializes a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">The booth's attendant.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            for (int i = 0; i < 10; i++)
            {
                this.Items.Add(new Map(0.5, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)));
            }

            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new CouponBook(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day), 0.8));
            }
        }

        /// <summary>
        /// Gives away a free coupon book.
        /// </summary>
        /// <returns>The given coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            CouponBook result = null;

            try
            {
                result = this.Attendant.FindItem(this.Items, typeof(CouponBook)) as CouponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Coupon book not found.", ex);
            }

            return result;
        }

        /// <summary>
        /// Gives away a free map.
        /// </summary>
        /// <returns>The given map.</returns>
        public Map GiveFreeMap()
        {
            Map result = null;

            try
            {
                result = this.Attendant.FindItem(this.Items, typeof(Map)) as Map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Map not found.", ex);
            }

            return result;
        }
    }
}