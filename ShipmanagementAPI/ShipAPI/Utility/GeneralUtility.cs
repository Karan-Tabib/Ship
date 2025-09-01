using Microsoft.EntityFrameworkCore;
using DTOs.Models;
using BusinessLayer.Abstraction;
namespace ShipAPI.Utility
{
    public static class GeneralUtility
    {
        public static async Task<int> GetUserID(HttpContext httpContext, IShipBL<User> repository)
        {
            var LoggedInuser = GetEmailID(httpContext);
            User userRecord = await repository.Get(user => user.Email == LoggedInuser);
            return userRecord.UserId;
        }

        public static string GetEmailID(HttpContext httpContext) {

            return httpContext.User.FindFirst("UserId").Value;
        }
        public static string GetUserId(HttpContext httpContext)
        {

            return httpContext.User.FindFirst("UserId").Value;
        }

    }
}
