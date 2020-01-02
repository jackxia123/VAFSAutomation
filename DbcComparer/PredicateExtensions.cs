using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbcComparer
{

        public static class PredicateExtensions
        {
            public static Predicate<T> And<T>
                (this Predicate<T> original, Predicate<T> newPredicate)
            {
                if (original == null && newPredicate == null)
                {
                    return t => true;
                }
                else if (original == null)
                {
                    return t => newPredicate(t);
                }
                else if (newPredicate == null)
                {
                    return t => original(t);
                }
                else
                {
                    return t => original(t) && newPredicate(t);
                }
               
            }

        public static Predicate<T> Or<T>
                (this Predicate<T> original, Predicate<T> newPredicate)
            {
                if (original == null && newPredicate == null)
                {
                    return t => true;
                }
                else if (original == null)
                {
                    return t => newPredicate(t);
                }
                else if (newPredicate == null)
                {
                    return t => original(t);
                }
                else
                {
                    return t => original(t) || newPredicate(t);
                }
            }


        public static Predicate<T> Or<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                foreach (Predicate<T> predicate in predicates)
                {
                    if (predicate(item))
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        public static Predicate<T> And<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                foreach (Predicate<T> predicate in predicates)
                {
                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }

}
