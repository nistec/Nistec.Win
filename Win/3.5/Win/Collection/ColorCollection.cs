using System;
using System.Drawing;
using System.Collections;

namespace MControl.Collections
{

	#region ColorCollection
	/// <summary>D�claration de l'interface IColorCollection</summary>
	public interface ColorCollection : IEnumerable
	{
		/// <summary>Obtient le nombre d'objets <see cref="Color"/>Color de la collection</summary>
		int Count { get; }
		/// <summary>Retourne l'objet couleur situ� � l'emplacement sp�cifi� dans la collection</summary>
		Color this[int i] { get; }
		/// <summary>Retourne l'objet couleur d�termin� par son nom</summary>
		Color this[string s] { get; }
		/// <summary>Obtient l'�num�rateur associ� � la collection</summary>
		/// <returns>Retourne un objet de type <seealso cref="IEnumerator"/>IEnumerator</see></returns>
		new IEnumerator GetEnumerator();
		/// <summary>Obtient l'index de la couleur sp�cifi�e dans la collection</summary>
		int IndexOf(string ColorName);
	}

	#endregion

	#region KnownColorCollection

	/// <summary>Enum�re les filtres applicables lors de la cr�ation d'une instance de la classe KnownColorCollection</summary>
	public enum KnownColorFilter 
	{
		/// <summary>Restreint les couleurs connues � celles utilis�es par le syst�me</summary>
		System,
		/// <summary>Restreint les couleurs connues � celles propos�es pour le web</summary>
		Web,
		/// <summary>Aucune restriction</summary>
		All
	};

	/// <summary>Classe de base qui encapsule les couleurs fournies par le Framework</summary>
    //[CLSCompliantAttribute(false)]
    public class KnownColorCollection : ColorCollection 
	{
		
		/// <summary>Contient le nombre d'objets que contient la collection</summary>
		protected readonly int COUNT;
		/// <summary>Contient l'index du premier �l�ment de la collection dans l'�num�ration KnownColor</summary>
		protected readonly int FIRST;
		/// <summary>Contient l'index du dernier �l�ment de la collection dans l'�num�ration KnownColor</summary>
		protected readonly int LAST;
		
		/// <summary>Constructeur unique</summary>
		public KnownColorCollection(KnownColorFilter filter) 
		{
			switch(filter) 
			{
				case KnownColorFilter.All: COUNT = 167; FIRST = 1; break;
				case KnownColorFilter.System: COUNT = 27; FIRST = 1; break;
				case KnownColorFilter.Web: COUNT = 140; FIRST = 28; break;
			}
			LAST = FIRST + COUNT;
		}
		/// <summary>Obtient le nombre d'objets <see cref="Color"/>Color de la collection</summary>
		public int Count 
		{
			get { return COUNT; }
		}

		/// <summary>Obtient l'�num�rateur associ� � la collection</summary>
		/// <returns>Retourne un �num�rateur<seealso cref="KnownColorEnumerator"/>KnownColorEnumerator</see></returns>
		public IEnumerator GetEnumerator() 
		{
			return new KnownColorEnumerator(this);
		}

		/// <summary>Retourne l'objet couleur situ� � l'emplacement sp�cifi� dans la collection</summary>
		/// <summary>Retourne l'objet couleur d�termin� par son nom</summary>
		public Color this[int iColor] 
		{
			get 
			{ 
				if(iColor < 0 || iColor >= Count) throw new ArgumentOutOfRangeException();
				return Color.FromKnownColor((KnownColor)(iColor + FIRST)); }
		}

		/// <summary>Retourne l'objet couleur d�termin� par son nom</summary>
		public Color this[string szColor] 
		{
			get 
			{
				if(szColor.Length == 0) throw new ArgumentNullException();
				return Color.FromName(szColor);
			} 
		}

		/// <summary>Obtient l'index de la couleur sp�cifi�e dans la collection</summary>
		public int IndexOf(string ColorName) 
		{
			for(int i=FIRST; i<LAST; i++) 
			{
				if(Color.FromKnownColor((KnownColor)i).Name.Equals(ColorName)) return i-FIRST;
			}
			return -1;
		}

		#region KnownColorEnumerator
		/// <summary>Enumerateur sp�cifique aux collections d'objets KnownColor</summary>
		private class KnownColorEnumerator : IEnumerator
		{
			/// <summary>Contient la position du curseur dans la collection</summary>
			private int m_Location;
			/// <summary>Contient une r�f�rence vers l'objet collection parent</summary>
			private ColorCollection m_ColorCollection;

			/// <summary>Constructeur</summary>
			/// <param name="colors">R�f�rence sur l'objet collection parent</param>
			public KnownColorEnumerator(ColorCollection colors) 
			{
				m_ColorCollection = colors;
				m_Location = -1;
			}
			/// <summary>D�place le curseur vers l'objet suivant dans la collection</summary>
			/// <returns>Renvoie true si l'op�ration a r�ussie et false sinon</returns>
			public bool MoveNext() 
			{
				return (++m_Location < m_ColorCollection.Count);
			}
			/// <summary>Obtient l'objet sur lequel est plac� le curseur</summary>
			/// <remarks>Une exception du type <see cref="InvalidOperationException"/>
			/// InvalidOperationException est lev�e si le curseur se trouve hors de la plage</remarks>
			public Object Current 
			{
				get 
				{
					if(m_Location < 0 || m_Location > m_ColorCollection.Count) throw new InvalidOperationException();
					return m_ColorCollection[m_Location];
				}
			}
			/// <summary>Replace le curseur en position initiale</summary>
			public void Reset() 
			{
				m_Location = -1;
			}
		}
		#endregion KnownColorEnumerator
	}

	#endregion

	#region CustomColorCollection

	/// <summary>Description r�sum�e de CustomColorCollection</summary>
	public class CustomColorCollection : ColorCollection
	{
		/// <summary>Liste contenant les objets de la collection</summary>
		private ArrayList m_Array;

		/// <summary>Constructeur unique</summary>
		public CustomColorCollection() 
		{
			m_Array = new ArrayList();
		}

		/// <summary>Impl�mente la fonction GetEnumerator de l'interface ColorCollection</summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator() 
		{
			return m_Array.GetEnumerator();
		}
		/// <summary>Obtient le nombre d'objets <see cref="Color"/>Color de la collection</summary>
		public int Count 
		{
			get { return m_Array.Count; }
		}
		/// <summary>Retourne l'objet couleur d�termin� par son index dans la collection</summary>
		public Color this[int iColor] 
		{
			get { return (Color) m_Array[iColor]; }
		}
		/// <summary>Retourne l'objet couleur d�termin� par son nom</summary>
		public Color this[string szColor] 
		{
			get 
			{ 
				if(szColor.Length == 0) throw new ArgumentNullException();
				return Color.FromName(szColor);
			}
		}
		/// <summary>Ajoute un objet Color � la collection</summary>
		/// <param name="color">Objet KnownColor � ajouter</param>
		public void Add(Color color) 
		{
			m_Array.Add(color);
		}
		/// <summary>Supprime un objet Color de la collection</summary>
		/// <param name="color">Objet KnownColor � supprimer</param>
		public void Remove(Color color)  
		{
			m_Array.Remove(color);
		}
		/// <summary>Supprime un objet Color de la collection</summary>
		/// <param name="index">Rang dans la collection de l'objet Color � supprimer</param>
		public void RemoveAt(int index)  
		{
			if(index < 0 || index >= m_Array.Count) throw new ArgumentOutOfRangeException();
			m_Array.RemoveAt(index);
		}

		/// <summary>Obtient le rang dans la collection d'une couleur de la collection</summary>
		/// <param name="ColorName">Nom de la couleur</param>
		/// <returns>Retourne le rang si la couleur se trouve dans la collection et -1 sinon</returns>
		public int IndexOf(string ColorName) 
		{
			return m_Array.IndexOf(ColorName);
		}

	}
	#endregion
}
