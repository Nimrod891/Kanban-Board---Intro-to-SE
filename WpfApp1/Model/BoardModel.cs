using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WpfApp1.Model;

namespace WpfApp1.Model
{
    class BoardModel: NotifableModelObject
    {

        private readonly UserModel _user;
        public  UserModel getUser()
        {
            return _user;
        }
        private ObservableCollection<string> _columns_names;
       // private readonly int _Id_board;
        private ObservableCollection<ColumnModel> _columns;
        //private int _columnId;

        
        public ObservableCollection<string> GetColumnsNames()
        {
            return _columns_names;
        }
        public ObservableCollection<ColumnModel> Columns { get=>_columns;
            set {
                _columns = value;
            }
        }

        private BoardModel(BackendController controller, ObservableCollection<ColumnModel> Columns) : base(controller)
        {
            this._columns = Columns;
            this._columns.CollectionChanged += HandleChange;
        }

        public BoardModel(BackendController controller, string email) : base(controller)
        {
            this._user = new UserModel(controller, email);
            this._columns_names = new ObservableCollection<string>(
                controller.GetBoard(email).Columns
               .Select((c, i) => controller.GetColumn(email, i).Value.Name));
            this._columns = new ObservableCollection<ColumnModel>(
                controller.GetBoard(email).Columns
               .Select((c, i) => new ColumnModel(controller, email, i, this.Controller.GetColumn(email, i).Value.Name, this.Controller.GetColumn(email, i).Value.Limit, this.Controller.GetColumn(email, i).Value.Tasks)));
            this.Columns.CollectionChanged += HandleChange;
        }


        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ColumnModel y in e.OldItems)
                {

                    Controller.RemoveColumn(_user.Email, y.ColumnOrdianl);
                }

            }
        }
    }
}
