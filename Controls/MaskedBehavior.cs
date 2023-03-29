using System;
namespace MControls
{
	public class MaskedBehavior: Behavior<Entry>
    {
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }
        /// <summary>
        /// Al agregar texto
        /// </summary>
        /// <param name="entry">Objeto que se esta modificando</param>
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
        /// <summary>
        /// Al eliminar texto
        /// </summary>
        /// <param name="entry">Objeto que se esta modificando</param>
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _positions;
        /// <summary>
        /// Agregar Caracteres
        /// </summary>
        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }
        /// <summary>
        /// Evento
        /// </summary>
        /// <param name="sender">Objeto con el que se trabaja</param>
        /// <param name="args">Parametros del eventos</param>
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (text.Length > _mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;

            entry.CursorPosition = entry.Text.Length;
        }
    }
}