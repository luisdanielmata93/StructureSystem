using System;
using System.Collections.Generic;
using System.Configuration;

namespace StructureSystem.Shared.Configuration
{
    public class WebConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Regulations", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(RegulationsCollection))]
        public RegulationsCollection Regulations
        {
            get { return (RegulationsCollection)base["Regulations"]; }
        }


        [ConfigurationProperty("Groups", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(GroupsCollection))]
        public GroupsCollection Groups
        {
            get { return (GroupsCollection)base["Groups"]; }
        }


        [ConfigurationProperty("Usages", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(UsagesCollection))]
        public UsagesCollection Usages
        {
            get { return (UsagesCollection)base["Usages"]; }
        }


        [ConfigurationProperty("Tests", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(TestsCollection))]
        public TestsCollection Tests
        {
            get { return (TestsCollection)base["Tests"]; }
        }


        [ConfigurationProperty("Materials", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(MaterialsCollection))]
        public MaterialsCollection Materials
        {
            get { return (MaterialsCollection)base["Materials"]; }
        }

    }//end of WebConfiguration class



    #region CollectionDefinitions
    public class RegulationsCollection : ConfigurationElementCollection
    {

        public ConfigurationObject this[int index]
        {
            get
            {
                return base.BaseGet(index) as ConfigurationObject;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        #region Add - Remove - Clear
        public void Add(ConfigurationObject element)
        {
            this.BaseAdd(element);
        }

        public void Remove(ConfigurationObject element)
        {
            BaseRemove(element.Id);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }
        #endregion

        #region ConfigurationElementCollection overrides
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationObject();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationObject)element).Id;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion

    } //end of class

    public class GroupsCollection : ConfigurationElementCollection
    {

        public ConfigurationObject this[int index]
        {
            get
            {
                return base.BaseGet(index) as ConfigurationObject;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        #region Add - Remove - Clear
        public void Add(ConfigurationObject element)
        {
            this.BaseAdd(element);
        }

        public void Remove(ConfigurationObject element)
        {
            BaseRemove(element.Id);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }
        #endregion

        #region ConfigurationElementCollection overrides
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationObject();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationObject)element).Id;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion

    } //end of class

    public class UsagesCollection : ConfigurationElementCollection
    {

        public ConfigurationObject this[int index]
        {
            get
            {
                return base.BaseGet(index) as ConfigurationObject;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        #region Add - Remove - Clear
        public void Add(ConfigurationObject element)
        {
            this.BaseAdd(element);
        }

        public void Remove(ConfigurationObject element)
        {
            BaseRemove(element.Id);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }
        #endregion

        #region ConfigurationElementCollection overrides
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationObject();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationObject)element).Id;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion

    } //end of class

    public class TestsCollection : ConfigurationElementCollection
    {

        public ConfigurationObject this[int index]
        {
            get
            {
                return base.BaseGet(index) as ConfigurationObject;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        #region Add - Remove - Clear
        public void Add(ConfigurationObject element)
        {
            this.BaseAdd(element);
        }

        public void Remove(ConfigurationObject element)
        {
            BaseRemove(element.Id);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }
        #endregion

        #region ConfigurationElementCollection overrides
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationObject();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationObject)element).Id;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion

    } //end of class

    public class MaterialsCollection : ConfigurationElementCollection
    {

        public ConfigurationObject this[int index]
        {
            get
            {
                return base.BaseGet(index) as ConfigurationObject;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        #region Add - Remove - Clear
        public void Add(ConfigurationObject element)
        {
            this.BaseAdd(element);
        }

        public void Remove(ConfigurationObject element)
        {
            BaseRemove(element.Id);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }
        #endregion

        #region ConfigurationElementCollection overrides
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationObject();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationObject)element).Id;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion

    } //end of class

    #endregion









    #region ConfigurationObject
    /// <summary>
    /// Cuerpo de objeto agregado a config.
    /// </summary>
    /// 
    public class ConfigurationObject : ConfigurationElement
    {

        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get { return (string)base["id"]; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }
    } //end of ConfigurationObject class

    #endregion


}//end of namespace
