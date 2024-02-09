using Moq;
using UserApp.Database.Model;
using UserApp.Database.Repository;

namespace TestUserApp
{
    public class AddUser
    {
        Mock<IRepositoryData> _mockRepositoryData;

        private void Init()
        {
            _mockRepositoryData = new Mock<IRepositoryData>();
        }


        [Fact]
        public void Test1()
        {
            var result = "";

            Init();
            Guid id = Guid.NewGuid();
            UserDetails user = new UserDetails();
            LinkUser linkUser = new LinkUser();
            //result =  _mockRepositoryData.SaveUser(user , linkUser);

            Assert.Null(result);
        }
    }
}