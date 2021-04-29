using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum Timestamp
    {
        /**
	 * The Always.
	 */
        [EnumMember(Value = "Always")] 
        ALWAYS

        ,
        /**
	 * The Workinghours.
	 */
        [EnumMember(Value = "Workinghours")] 
        WORKINGHOURS
    }
}