﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PLM.Models;

namespace PLM
{
    public class ModuleViewModel
    {
        public List<Module> Mods = new List<Module>();
        public List<Category> Cats = new List<Category>();
    }
    public class DisableModuleViewModel
    {
        public DisableModuleViewModel() { }

        public DisableModuleViewModel(Module module)
        {
            ModuleID = module.ModuleID;
            Name = module.Name;
            DisableReason = module.DisableReason;
            DisableModuleNote = module.DisableModuleNote;
            isDisabled = module.isDisabled;
        }

        [Key]
        [Display(Name = "Module Name")]
        public string Name { get; set; }
        
        [Display(Name = "Module ID")]
        public int ModuleID { get; set; }

        [Display(Name = "Is Disabled")]
        public bool isDisabled { get; set; }

        [Display(Name = "Note")]
        public string DisableModuleNote { get; set; }

        [Display(Name = "Reason")]
        public Module.DisableModuleReason DisableReason { get; set; }
    }

    //TODO: COPY OVER
    /// <summary>
    /// Used in pvModuleFilterList (Modules Index)
    /// </summary>
    public class ModuleFilterMenuList
    {
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Key]
        [Display(Name = "CategoryID")]
        public virtual int CategoryId { get; set; }

        [Display(Name = "Module Count")]
        public int ModuleCount { get; set; }

    }
}