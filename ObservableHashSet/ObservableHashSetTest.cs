namespace BellaCode.UnitTests
{
    using System.Collections.Specialized;
    using BellaCode.Collections.ObjectModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class ObservableHashSetTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInitialized()
        {
            // Arrange

            // Act
            var actual = new ObservableHashSet<int>();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
            Assert.IsNotNull(actual.Comparer);
        }

        #region Add Tests

        [TestMethod()]
        public void WhenAdd_ThenCountUpdated()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            // Act
            target.Add(5);

            var actual = target.Count;

            // Assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod()]
        public void WhenAdd_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Contains(5))
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.Add(5);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenAdd_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.Add(5);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        #endregion

        #region Clear Tests

        [TestMethod()]
        public void WhenClear_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.Clear();

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenClear_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.Clear();

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        #endregion

        #region ExceptWith Tests

        [TestMethod()]
        public void WhenExceptWith_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 2, 4, 5, 6 };

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove &&
                    e.OldItems.Count == 2 &&
                    e.OldItems.Contains(2) &&
                    e.OldItems.Contains(4))
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.ExceptWith(other);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenExceptWith_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 2, 4, 5, 6 };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.ExceptWith(other);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenExceptWithAndOtherDisjoint_ThenDoesNotRaiseCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 5, 6 };

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.ExceptWith(other);

            // Assert
            Assert.IsFalse(wasEventRaised);
        }

        [TestMethod()]
        public void WhenExceptWithAndOtherDisjoint_ThenDoesNotRaiseCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 5, 6 };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.ExceptWith(other);

            // Assert
            Assert.IsFalse(wasEventRaised);
        }

        #endregion

        #region IntersectWith Tests

        [TestMethod()]
        public void WhenIntersectsWith_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 2, 4, 5, 6 };

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove &&
                    e.OldItems.Count == 2 &&
                    e.OldItems.Contains(1) &&
                    e.OldItems.Contains(3))
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.IntersectWith(other);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenIntersectsWith_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 2, 4, 5, 6 };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.IntersectWith(other);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        #endregion

        #region Remove Tests

        [TestMethod()]
        public void WhenRemove_ThenCountUpdated()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            // Act
            target.Remove(3);

            var actual = target.Count;

            // Assert
            Assert.AreEqual(3, actual);
        }

        [TestMethod()]
        public void WhenRemove_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(3))
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.Remove(3);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenRemove_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.Remove(3);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        #endregion

        #region SymmetricExceptWith Tests

        [TestMethod()]
        public void WhenSymmetricExceptWith_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1, 4, 5, 6 };

            bool wasRemoveEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove &&
                    e.OldItems.Count == 2 &&
                    e.OldItems.Contains(1) &&
                    e.OldItems.Contains(4))
                {
                    wasRemoveEventRaised = true;
                }
            };

            bool wasAddEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add &&
                    e.NewItems.Count == 2 &&
                    e.NewItems.Contains(5) &&
                    e.NewItems.Contains(6))
                {
                    wasAddEventRaised = true;
                }
            };

            // Act
            target.SymmetricExceptWith(other);

            // Assert
            Assert.IsTrue(wasRemoveEventRaised);
            Assert.IsTrue(wasAddEventRaised);
        }

        [TestMethod()]
        public void WhenSymmetricExceptWith_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1, 4, 5, 6 };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.SymmetricExceptWith(other);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenSymmetricExceptWithAndOtherDisjoint_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 5, 6 };

            bool wasRemoveEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    wasRemoveEventRaised = true;
                }
            };

            bool wasAddEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add &&
                    e.NewItems.Count == 2 &&
                    e.NewItems.Contains(5) &&
                    e.NewItems.Contains(6))
                {
                    wasAddEventRaised = true;
                }
            };

            // Act
            target.SymmetricExceptWith(other);

            // Assert
            Assert.IsFalse(wasRemoveEventRaised);
            Assert.IsTrue(wasAddEventRaised);
        }        

        [TestMethod()]
        public void WhenSymmetricExceptWithAndOtherEquals_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1, 2, 3, 4 };

            bool wasRemoveEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove &&
                    e.OldItems.Count == 4 &&
                    e.OldItems.Contains(1) &&
                    e.OldItems.Contains(2) &&
                    e.OldItems.Contains(3) &&
                    e.OldItems.Contains(4))
                {
                    wasRemoveEventRaised = true;
                }
            };

            bool wasAddEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    wasAddEventRaised = true;
                }
            };

            // Act
            target.SymmetricExceptWith(other);

            // Assert
            Assert.IsTrue(wasRemoveEventRaised);
            Assert.IsFalse(wasAddEventRaised);
        }

        [TestMethod()]
        public void WhenSymmetricExceptWithAndOtherEmpty_ThenDoesNotRaiseCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] {  };

            bool wasRemoveEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    wasRemoveEventRaised = true;
                }
            };

            bool wasAddEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    wasAddEventRaised = true;
                }
            };

            // Act
            target.SymmetricExceptWith(other);

            // Assert
            Assert.IsFalse(wasRemoveEventRaised);
            Assert.IsFalse(wasAddEventRaised);
        }


        [TestMethod()]
        public void WhenSymmetricExceptWithAndOtherEmpty_ThenDoesNotRaiseCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.UnionWith(other);

            // Assert
            Assert.IsFalse(wasEventRaised);
        }

        #endregion

        #region UnionWith Tests        

        [TestMethod()]
        public void WhenUnionWith_ThenRaisesCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1, 4, 5, 6 };           

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add &&
                    e.NewItems.Count == 2 &&
                    e.NewItems.Contains(5) &&
                    e.NewItems.Contains(6))
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.UnionWith(other);

            // Assert            
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenUnionWith_ThenRaisesCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1, 4, 5, 6 };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.UnionWith(other);

            // Assert
            Assert.IsTrue(wasEventRaised);
        }

        [TestMethod()]
        public void WhenUnionWithAndOtherEqual_ThenDoesNotRaiseCollectionChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1,2,3,4 };

            bool wasEventRaised = false;
            target.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.UnionWith(other);

            // Assert            
            Assert.IsFalse(wasEventRaised);
        }

        [TestMethod()]
        public void WhenUnionWithAndOtherEqual_ThenDoesNotRaiseCountPropertyChanged()
        {
            // Arrange
            var target = new ObservableHashSet<int>();

            target.Add(1);
            target.Add(2);
            target.Add(3);
            target.Add(4);

            var other = new int[] { 1, 2, 3, 4 };

            bool wasEventRaised = false;
            target.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ObservableHashSet<int>.PropertyNames.Count)
                {
                    wasEventRaised = true;
                }
            };

            // Act
            target.UnionWith(other);

            // Assert
            Assert.IsFalse(wasEventRaised);
        }

        #endregion
    }
}
