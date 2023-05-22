using BloodBankManagement.Server.Models;
using BloodBankManagement.Shared;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodBankManagement.Server.Services
{
	public class DonerManager : IDoner
	{
		readonly BloodHospitalContext _dbContext = new();
		public DonerManager(BloodHospitalContext dbContext) 
		{ 
			_dbContext = dbContext;
		}
        //take all list from db
		public List<Doner> DataDoner()
		{
			try
			{
				return _dbContext.Doners.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
        //remove a doner from db
		public void DeleteDoner(int id)
		{
			try
			{
				Doner? doner = _dbContext.Doners.Find(id);
				if (doner != null)
				{
					_dbContext.Doners.Remove(doner);
					_dbContext.SaveChanges();
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
        //get doner from db
		public Doner GetDoner(int id)
		{
			try
			{
				Doner? doner = _dbContext.Doners.Find(id);
				if(doner != null)
				{
					return doner;
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
        //create new doner
		public void NewDoner(Doner doner)
		{
			try
			{
				_dbContext.Add(doner);
				_dbContext.SaveChanges();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
        //edit a doner
		public void UpdateDoner(Doner doner)
		{
			try
			{

				_dbContext.Entry(doner).State = EntityState.Modified;
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
        //create a list of receivers who a person can donate
		public List<Doner> CanDonateTo(Doner doner)
		{
			List<Doner> listReceivers = new();
			List<Doner > listDoners = _dbContext.Doners.ToList();
			switch (doner.BloodType)
			{
				case "O+":
					foreach (var d in listDoners)
					{
						if (d.BloodType.Contains("-") || d.Id == doner.Id)
						{
							continue;
						}
						else
						{
                            listReceivers.Add(d);
						}
					}
					break;
				case "O-":
					foreach (var d in listDoners)
					{
						if(d.Id == doner.Id)
						{
							continue;
						}
						else
						{
                            listReceivers.Add(d);
						}
					}
					break;
				case "A+":
					foreach (var d in listDoners)
					{
						if(!d.BloodType.Contains("AB")  || d.BloodType == "A-" || d.Id == doner.Id)
						{
                            continue;
                        }
						else
						{
                            listReceivers.Add(d);
                        }
					}
					break;
				case "A-":
                    foreach (var d in listDoners)
                    {
                        if (!d.BloodType.Contains("A") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listReceivers.Add(d);
                        }
                    }
                    break;
                case "AB+":
                    foreach (var d in listDoners)
                    {
                        if (!d.BloodType.Contains("AB+") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listReceivers.Add(d);
                        }
                    }
                    break;
                case "AB-":
                    foreach (var d in listDoners)
                    {
                        if (!d.BloodType.Contains("AB") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listReceivers.Add(d);
                        }
                    }
                    break;
                case "B+":
                    foreach (var d in listDoners)
                    {
                        if (!d.BloodType.Contains("B+") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listReceivers.Add(d);
                        }
                    }
                    break;
                case "B-":
                    foreach (var d in listDoners)
                    {
                        if (!d.BloodType.Contains("B") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listReceivers.Add(d);
                        }
                    }
                    break;
                default:
					new ArgumentNullException();
					break;
			}
			return listReceivers;
		}
        //create a list of donors who a person can receive
        public List<Doner> CanReceiveFrom(Doner doner)
        {
            List<Doner> listDonors = new();
            List<Doner> listDoners = _dbContext.Doners.ToList();
            switch (doner.BloodType)
            {
                case "O+":
                    foreach (var d in listDoners)
                    {
                        if (!d.BloodType.Contains("O") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "O-":
                    foreach (var d in listDoners)
                    {
                        if (d.BloodType != "O-" || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "A+":
                    foreach (var d in listDoners)
                    {
                        if (d.BloodType.Contains("B") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "A-":
                    foreach (var d in listDoners)
                    {
                        if (d.BloodType != "O-" || d.BloodType != "A-" || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "AB+":
                    foreach (var d in listDoners)
                    {
                        if (d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "AB-":
                    foreach (var d in listDoners)
                    {
                        if (d.BloodType.Contains("+")  || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "B+":
                    foreach (var d in listDoners)
                    {
                        if (d.BloodType.Contains("A") || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                case "B-":
                    foreach (var d in listDoners)
                    {
                        if (d.BloodType != "O-" || d.BloodType != "B-" || d.Id == doner.Id)
                        {
                            continue;
                        }
                        else
                        {
                            listDonors.Add(d);
                        }
                    }
                    break;
                default:
                    new ArgumentNullException();
                    break;
            }
            return listDonors;
        }
    }
}
