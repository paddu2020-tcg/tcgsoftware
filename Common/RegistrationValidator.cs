using System;
using System.Data;
using System.Text.RegularExpressions;

namespace YamahaApp.Registration.Validation
{
	/// <summary>
	/// Summary description for RegistrationValidator.
	/// </summary>
	/// 
	
	public delegate bool CustomValidationHandler (string filedName, DataTable objErrorMessgaes, DataView objCoreData, DataView objProfileData, DataView objInitiativeData, string type);

	public class RegistrationValidator
	{
        public event CustomValidationHandler CustomValidation;

		public RegistrationValidator()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Validate Method
		public bool Validate(DataView objCore, DataView objProfile, DataView objInitiative, DataView objValidationRules, out DataTable objReturn)
		{
			objReturn = new DataTable();
			objReturn.Columns.Add("type",System.Type.GetType("System.String"));
			objReturn.Columns.Add("column",System.Type.GetType("System.String"));
			objReturn.Columns.Add("Message",System.Type.GetType("System.String"));

			bool valid;
			bool returnValue = true;
			string filedName = string.Empty;
			string validationType = string.Empty;
			string errorMessage = string.Empty;
			string type = string.Empty;
			DataRowView objCurrent;
			foreach(DataRowView objRow in objValidationRules)
			{
				valid = true;
				type = objRow["Type"].ToString().ToLower();
				filedName = objRow["Name"].ToString().ToLower();
				validationType = objRow["Validation"].ToString().ToLower();
				errorMessage = objRow["Message"].ToString();
				if(objReturn.Rows.Count != 0)
				{
					objCurrent = RowLookup(filedName, objReturn.DefaultView, null, null, type);
					if(objCurrent != null)
					{
						valid = false;
					}
				}
				if(valid)
				{
					if(objRow["Validation"].ToString().ToLower() == "custom")
					{
						valid = this.onCustomValidation(filedName, objReturn, objCore, objProfile, objInitiative, type);
					}
					else
					{
						objCurrent = RowLookup(filedName, objCore, objProfile, objInitiative, type);
						if(objCurrent != null)
						{
							switch(validationType)
							{
								case "email" :
									valid = ValidateEmail(objCurrent["value"].ToString());
									break;
								case "zip" :
									valid = ValidateZip(objCurrent["value"].ToString());
									break;
								case "mandatory" :
									valid = ValidateMandatory(objCurrent["value"].ToString());
									break;
								default:
									break;
							}
						}
						else
						{
							//field not present in data
							if(validationType == "mandatory")
							{
								valid = false;
							}
						}
						//Check for error and filed name to return dataview
						if(!valid)
						{
							DataRow obrow = objReturn.NewRow();
							obrow["type"] = type;
							obrow["column"] = filedName;
							obrow["Message"] = errorMessage;
							objReturn.Rows.Add(obrow);
						}
					}
				}
			}
			if(objReturn.Rows.Count >= 1)
			{
				returnValue = false;
			}
			return returnValue;
		}
		#endregion

		#region Event onCustomValidation
		protected bool onCustomValidation(string filedName, DataTable objErrorMessgaes, DataView objCoreData, DataView objProfileData, DataView objInitiativeData, string type)
		{
			bool returnVal = false;
			if(this.CustomValidation != null)
			{
				returnVal = this.CustomValidation(filedName, objErrorMessgaes, objCoreData, objProfileData, objInitiativeData, type);
			}
			return returnVal;
		}
		#endregion

		#region RowLooup Method
		public static DataRowView RowLookup(string colName, DataView objCore, DataView objProfile, DataView objInitiative, string type)
		{
			bool found = false;
			DataRowView returnRowView = null;
			if(objCore != null && type=="core")
			{
				foreach (DataRowView current in objCore)
				{
					if(current["column"].ToString().ToLower() == colName)
					{
						returnRowView = current;
						found = true;
						break;
					}
				}
			}
			else if(!found && objProfile != null && type=="profile")
			{
				foreach (DataRowView current in objProfile)
				{
					if(current["column"].ToString().ToLower() == colName)
					{
						returnRowView = current;
						found = true;
						break;
					}
				}
			}
			else if(!found && objInitiative != null && type=="initiative")
			{
				foreach (DataRowView current in objInitiative)
				{
					if(current["column"].ToString().ToLower() == colName)
					{
						returnRowView = current;
						found = true;
						break;
					}
				}
			}
            return returnRowView;
		}
		#endregion

		#region Field Validation Methods for email, zip, mandatory
		private static bool ValidateZip(string zip)
		{
			string pattern = @"^\d{5}$";
			Regex reg = new Regex(pattern);
			return reg.IsMatch(zip);
		}

		private static bool ValidateEmail(string email)
		{
			string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
			Regex reg = new Regex(pattern);
			return reg.IsMatch(email);
		}

		private static bool ValidateMandatory(string filedValue)
		{
			bool returnValue = false;
			if(filedValue != "" && filedValue != string.Empty)
			{
				returnValue = true;
			}
			return returnValue;
		}
		#endregion
	}
}
