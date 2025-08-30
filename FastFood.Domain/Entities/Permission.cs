//using FastFood.Domain.Exceptions;

//namespace FastFood.Domain.Entities
//{
//    public class Permission
//    {
//        #region Properties
        
//        public int Id { get; private set; }
//        public string Name { get; private set; }

//        #endregion

//        private Permission()
//        {
            
//        }

//        private Permission(string name)
//        {
//            Name = name;
//        }

//        public static Permission Create(string name)
//        {
//            return new Permission(name);
//        }

//        public void UpdatePermission(string name)
//        {
//            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
//                throw new DomainException("Nome de permissão inválido");
//            Name = name;
//        }

//        public bool ValidName(string name)
//        {
//            if (string.IsNullOrEmpty(name))
//                throw new DomainException("Nome de permissão inválido");

//            if(string.IsNullOrWhiteSpace(name))
//                throw new DomainException("Nome de permissão inválido");

//            return true;
//        }
//    }

//}
