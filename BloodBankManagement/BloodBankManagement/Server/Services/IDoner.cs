using BloodBankManagement.Shared;

namespace BloodBankManagement.Server.Services
{
	public interface IDoner
	{
		public List<Doner> DataDoner();
		public void NewDoner(Doner doner);
		public void DeleteDoner(int id);
		public Doner GetDoner(int id);
		public void UpdateDoner(Doner doner);
		public List<Doner> CanDonateTo(Doner doner);
		public List<Doner> CanReceiveFrom(Doner doner);
	}
}
