using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace XPlatform.Core.Controls
{
    public class EventToCommand : TriggerAction<VisualElement>
    {
		public EventToCommandDelegate Delegate {
			get;
			set;
		}

        protected override void Invoke(VisualElement parameter)
        {
            if (this.AssociatedType == null)
            {
                return;
            }

			this.Delegate.BindingContext = parameter.BindingContext;

            ICommand command = this.Delegate.Command;
            object param = this.Delegate.CommandParameter ?? parameter;
            if (command != null && command.CanExecute(param))
            {
                command.Execute(param);
            }
        }
    }

	public class EventToCommandDelegate : BindableObject
	{
		public static readonly BindableProperty CommandProperty = 
			BindableProperty.Create<EventToCommandDelegate,ICommand>(e => e.Command, null);
		public static readonly BindableProperty CommandParameterProperty = 
			BindableProperty.Create<EventToCommandDelegate,object>(e => e.CommandParameter, null);

		public ICommand Command
		{
			get
			{
				return (ICommand)GetValue(CommandProperty);
			}
			set
			{
				SetValue (CommandProperty, value);
			}
		}

		public object CommandParameter
		{
			get
			{
				return (object)GetValue(CommandParameterProperty);
			}
			set
			{
				SetValue (CommandParameterProperty, value);
			}
		}
	}
}
