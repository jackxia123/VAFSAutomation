using System;
using System.Linq;
using System.Reflection;

namespace CANTxGenerator
{
    static class Utility
    {
        //Deep copy with reflection
        public static object Copy(this object obj)
        {
            Object targetDeepCopyObj;
            Type targetType = obj.GetType();
            //if value type, return directly  
            if (targetType.IsValueType == true)
            {
                targetDeepCopyObj = obj;
            }
            //ref type   
            else
            {
                targetDeepCopyObj = System.Activator.CreateInstance(targetType);   //new instance   
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, Copy(fieldValue));
                        }

                    }
                    else if (member.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;
                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            object propertyValue;
                            int noOfParameters = myProperty.GetIndexParameters().Count();
                            //if (noOfParameters == 0)
                            //{
                            //Non-Indexed PROPERTY
                            propertyValue = myProperty.GetValue(obj, null);
                            //}
                            //else
                            //{
                            //Indexed PROPERTY

                            //object[] indexArgs = new object[((IList)myProperty).Count];
                            //for (int i = 0; i < ((IList)myProperty).Count; i++)
                            //{
                            //    indexArgs[i] = i;
                            //}
                            //    propertyValue = myProperty.GetValue(obj, new object[] { 0});
                            //}

                            if (propertyValue is ICloneable)
                            {
                                myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                            }
                            else
                            {
                                myProperty.SetValue(targetDeepCopyObj, Copy(propertyValue), null);
                            }
                        }

                    }
                }
            }
            return targetDeepCopyObj;
        }


    }
}
