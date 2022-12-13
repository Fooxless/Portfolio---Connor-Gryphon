namespace MovieManage
{
	// Phase 2
	// An implementation of MovieCollection ADT
	// 2022

	using System;

	//A class that models a node of a binary search tree
	//An instance of this class is a node in a binary search tree 
	public class BTreeNode
	{
		private IMovie movie; // movie
		private BTreeNode lchild; // reference to its left child 
		private BTreeNode rchild; // reference to its right child

		public BTreeNode(IMovie movie)
		{
			this.movie = movie;
			lchild = null;
			rchild = null;
		}

		public IMovie Movie
		{
			get { return movie; }
			set { movie = value; }
		}

		public BTreeNode LChild
		{
			get { return lchild; }
			set { lchild = value; }
		}

		public BTreeNode RChild
		{
			get { return rchild; }
			set { rchild = value; }
		}
	}

	// invariant: no duplicates in this movie collection
	public class MovieCollection : IMovieCollection
	{
		private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
		private int count; // the number of (different) movies currently stored in this movie collection 



		// get the number of movies in this movie colllection 
		// pre-condition: nil
		// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
		public int Number { get { return count; } }

		// constructor - create an object of MovieCollection object
		public MovieCollection()
		{
			root = null;
			count = 0;
		}

		// Check if this movie collection is empty
		// Pre-condition: nil
		// Post-condition: return true if this movie collection is empty; otherwise, return false.
		public bool IsEmpty()
		{
			if (count == 0)
			{
				return true;
			}
			return false;
		}

		// Insert a movie into this movie collection
		// Pre-condition: nil
		// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
		public bool Insert(IMovie movie)
		{
			if (root == null)
			{
				// Create first node
				BTreeNode parent = new BTreeNode(movie);
				root = parent;
				count++;
				return true;
			}
			else
			{
				// Insert any other node
				return recursiveInsert(movie, root);
			}
		}
		private bool recursiveInsert(IMovie Key, BTreeNode root)
		{
			int compareresult = root.Movie.CompareTo(Key);
			if (compareresult == 1)
			{
				if (root.LChild == null)
				{
					// Insert Left
					BTreeNode parent = new BTreeNode(Key);
					root.LChild = parent;
					count++;
					return true;
				}
				else
				{
					// Left is full
					return recursiveInsert(Key, root.LChild);
				}
			}
			else if (compareresult == -1)
			{
				if (root.RChild == null)
				{
					// Insert Right
					BTreeNode parent = new BTreeNode(Key);
					root.RChild = parent;
					count++;
					return true;
				}
				else
				{
					// Right is full
					return recursiveInsert(Key, root.RChild);
				}
			}
			return false;
		}

		// Delete a movie from this movie collection
		// Pre-condition: nil
		// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
		public bool Delete(IMovie movie)
		{
			BTreeNode ptr = root;
			BTreeNode parent = null;

			while ((ptr != null) && (movie.CompareTo(ptr.Movie) != 0))
			{
				parent = ptr;
				if (movie.CompareTo(ptr.Movie) < 0) { ptr = ptr.LChild; }// move to the left child of ptr

				else { ptr = ptr.RChild; }

			}

			if (ptr != null) // if the search was successful
			{
				// case 3: item has two children
				if ((ptr.LChild != null) && (ptr.RChild != null))
				{
					// find the right-most node in left subtree of ptr
					if (ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
					{
						ptr.Movie = ptr.LChild.Movie;
						ptr.LChild = ptr.LChild.LChild;
						count--;
						return true;
					}
					else
					{
						BTreeNode p = ptr.LChild;
						BTreeNode pp = ptr; // parent of p
						while (p.RChild != null)
						{
							pp = p;
							p = p.RChild;
						}
						// copy the item at p to ptr
						ptr.Movie = p.Movie;
						pp.RChild = p.LChild;
						count--;
						return true;
					}
				}
				else // cases 1 & 2: item has no or only one child
				{
					BTreeNode c;
					if (ptr.LChild != null) { c = ptr.LChild; }

					else { c = ptr.RChild; }


					// remove node ptr
					if (ptr == root)
					{
						root = c;
						count--;
						return true;
					}//need to change root

					else
					{
						if (ptr == parent.LChild) { parent.LChild = c; }

						else { parent.RChild = c; }
						count--;
						return true;
					}
				}

			}
			return false;
		}

		// Search for a movie in this movie collection
		// pre: nil
		// post: return true if the movie is in this movie collection;
		//	     otherwise, return false.
		public bool Search(IMovie movie)
		{
			return recursiveSearch(movie, root);

		}
		private bool recursiveSearch(IMovie Key, BTreeNode root)
		{
			if (root != null)
			{
				int compareresult = root.Movie.CompareTo(Key);
				if (compareresult == 0)
				{
					return true;
				}
				else
				{
					if (compareresult == -1)
					{
						return recursiveSearch(Key, root.RChild);
					}
					else
					{
						return recursiveSearch(Key, root.LChild);
					}
				}
			}
			else { return false; }
		}

		// Search for a movie by its title in this movie collection  
		// pre: nil
		// post: return the reference of the movie object if the movie is in this movie collection;
		//	     otherwise, return null.
		public IMovie Search(string movietitle)
		{
			return recursiveSearch(movietitle, root);

		}
		private IMovie recursiveSearch(string Key, BTreeNode root)
		{
			if (root != null)
			{
				int compareresult = root.Movie.Title.CompareTo(Key);
				if (compareresult == 0)
				{
					return root.Movie;
				}
				else
				{
					if (compareresult == -1)
					{
						return recursiveSearch(Key, root.RChild);
					}
					else
					{
						return recursiveSearch(Key, root.LChild);
					}
				}
			}
			else { return null; }
		}

		// Store all the movies in this movie collection in an array in the dictionary order by their titles
		// Pre-condition: nil
		// Post-condition: return an array of movies that are stored in dictionary order by their titles
		public IMovie[] ToArray()
		{
			int num = 0;
			IMovie[] array = new IMovie[count];
			InOrder(root);

			return array;

			void InOrder(BTreeNode root)
			{
				if (root != null)
				{
					InOrder(root.LChild);
					array[num] = root.Movie;
					num++;
					InOrder(root.RChild);
				}
			}
		}

		// Clear this movie collection
		// Pre-condotion: nil
		// Post-condition: all the movies have been removed from this movie collection 
		public void Clear()
		{
			// Clear the Movies
			root = null;
			count = 0;
		}
	}
}

