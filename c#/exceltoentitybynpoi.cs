using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Reflection;
namespace OrderManager.Infrastructure.CrossCutting
{
   

    public class ExcelToEntityByNpoi<TEntity> where TEntity:class
    {

        private HSSFWorkbook _hssfworkbook;
        private IList<string> _nameList;    
        public void InitializeWorkbook(string path)
        {
           
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                _hssfworkbook = new HSSFWorkbook(file);
            }
        }
         
        

     
        public IList<TEntity> ImportToEntity()
        {
            List<TEntity> valueList = new List<TEntity>();
            ISheet sheet = _hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
           
                while (rows.MoveNext())
                {
                    IRow row = (HSSFRow)rows.Current;
                    TEntity entity = System.Activator.CreateInstance<TEntity>();
                    for (int i = 0; i < _nameList.Count; i++)
                    {
                        ICell cell = row.GetCell(i);
                        string cellName = _nameList[i];
                        string cellValue;
                        if (cell == null)
                        {
                            cellValue = "";
                        }
                        else
                        {
                            cellValue = cell.ToString();
                        }
                        this.SetValueToEntity(entity, cellName, cellValue);
                    }
                    valueList.Add(entity);

                }
          

            return valueList;
        }
        public void SetValueToEntity(TEntity entity,string name,string value)
        {
            Type type = (entity as object).GetType();
            PropertyInfo propertyInfo = type.GetProperty(name);
            if (propertyInfo==null)
            {
                throw new Exception(string.Format("{0}属性查找失败!", name));
            }
            if (!propertyInfo.PropertyType.IsGenericType)
            {
                propertyInfo.SetValue(entity, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, propertyInfo.PropertyType), null);
            }
            else
            {
                Type genericTypeDefinition = propertyInfo.PropertyType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    propertyInfo.SetValue(entity, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value,
                        Nullable.GetUnderlyingType(propertyInfo.PropertyType)), null);
                }
            }
           
                     
            
        }
       
        public IList<TEntity> ImportToEntity(List<string> nameList)
        {
            _nameList = nameList;
            return ImportToEntity();                       
        }
        
    }
 
    
}
