using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxUsersManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task CreateEnterpriseUserAsync_ForCorrectUserRequest_ShouldCreateNewApplicationUser()
        {
            var name = GetUniqueName("user");
            var userRequest = new BoxUserRequest
            {
                Name = name,
                IsPlatformAccessOnly = true,
                ExternalAppUserId = GetUniqueName("user-id")
            };

            var newUser = await AdminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);

            Assert.AreEqual(newUser.Name, name);

            await AdminClient.UsersManager.DeleteEnterpriseUserAsync(newUser.Id, false, true);
        }

        [TestMethod]
        public async Task UpdateUserInformationAsync_ForNewExternalAppId_ShouldUpdateExistingUser()
        {
            var user = await CreateEnterpriseUser();
            var newName = GetUniqueName("username");
            var updateUserRequest = new BoxUserRequest
            {
                Id = user.Id,
                Name = newName
            };

            var updatedUser = await AdminClient.UsersManager.UpdateUserInformationAsync(updateUserRequest);

            Assert.AreEqual(updatedUser.Name, newName);
        }

        [TestMethod]
        public async Task GetEnterpriseUsersAsync_ForNewExternalAppId_ShouldUpdateExistingUser()
        {
            var user = await CreateEnterpriseUser();

            var appUsers = await AdminClient.UsersManager.GetEnterpriseUsersAsync();

            Assert.IsTrue(appUsers.Entries.Any(x => x.Id == user.Id));
        }

        [TestMethod]
        public async Task DeleteEnterpriseUserAsync_ForExistingUser_ShouldDeleteThisUserFromBox()
        {
            var userRequest = new BoxUserRequest
            {
                Name = GetUniqueName("user"),
                IsPlatformAccessOnly = true,
                ExternalAppUserId = GetUniqueName("user-id")
            };
            var newUser = await AdminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);

            await AdminClient.UsersManager.DeleteEnterpriseUserAsync(newUser.Id, false, true);

            var appUsers = await AdminClient.UsersManager.GetEnterpriseUsersAsync();
            Assert.IsFalse(appUsers.Entries.Any(item => item.Id == newUser.Id));
        }
    }
}
