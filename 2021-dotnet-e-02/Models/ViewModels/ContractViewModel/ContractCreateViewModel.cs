using _2021_dotnet_e_02.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models.ViewModels.ContractViewModel
{
    public class ContractCreateViewModel
    {
        [Required(ErrorMessage = "ContractType is required")]
        [Display(Name = "Contract type")]
        [EnumDataType(typeof(ContractTypeStatus))]
        public ActemiumContractType ContractType { get; set; }
        [Display(Name = "Contract status")]
        public string Status { get; set; }
        [Required(ErrorMessage = "StartDate is required")]
        [Display(Name = "Contract start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        [Range(1,3, ErrorMessage = "Duration has to be 1, 2 or 3 years")]
        [Display(Name = "Contract duration")]
        public int Duration { get; set; }

        public ContractCreateViewModel()
        {

        }
        public ContractCreateViewModel(ActemiumContract contract)
        {
            ContractType = contract.ContractType;
            StartDate = contract.StartDate;
        }
    }
}
