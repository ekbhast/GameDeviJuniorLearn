using System;
using System.Collections.Generic;

namespace Deck_of_cards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            dealer.Play();
        }
    }

    class Dealer
    {
        private Deck _deck;
        private Player _player;

        public Dealer()
        {
            DeckFactory deckFactory = new DeckFactory();
            _deck = new Deck(deckFactory.GetNew());
            _player = new Player();
        }

        public void GiveCards(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                _player.ReceiveCard(_deck.GiveCard());
            }
        }

        public void Play()
        {
            Console.WriteLine("Введите сколько карт вам нужно");

            bool isNumber = false;

            while (isNumber == false)
            {
                if (isNumber = int.TryParse(Console.ReadLine(), out int numberOfCards))
                {
                    if (_deck.Size < numberOfCards)
                    {
                        Console.WriteLine("В колоде не достаточно карт");
                    }
                    else
                    {
                        GiveCards(numberOfCards);
                        _deck.ShowCards();
                        _player.ShowCards();
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели не число!");
                }
            }
        }
    }

    class Deck
    {
        private Stack<Card> _cards = new Stack<Card>();

        public Deck(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                _cards.Push(new Card(card.Name, card.Suit));
            }
        }

        public int Size => _cards.Count;

        public Card GiveCard()
        {
            return _cards.Pop();
        }

        public void ShowCards()
        {
            Console.WriteLine("Карты оставлшиеся в колоде:");

            foreach (Card card in _cards)
            {
                Console.WriteLine($"{card.Name} - {card.Suit}");
            }
        }
    }

    class DeckFactory
    {
        private List<Card> _cards = new List<Card>();

        public DeckFactory()
        {
            _cards.Add(new Card("Туз", "Червы"));
            _cards.Add(new Card("Король", "Червы"));
            _cards.Add(new Card("Дама", "Червы"));
            _cards.Add(new Card("Валет", "Червы"));
            _cards.Add(new Card("Десятка", "Червы"));

            _cards.Add(new Card("Девятка", "Бубны"));
            _cards.Add(new Card("Восьмёрка", "Бубны"));
            _cards.Add(new Card("Семёрка", "Бубны"));
            _cards.Add(new Card("Шестёрка", "Бубны"));
            _cards.Add(new Card("Пятёрка", "Бубны"));
        }

        public List<Card> GetNew()
        {
            return _cards;
        }
    }

    class Card
    {
        public Card(string name, string suit)
        {
            Name = name;
            Suit = suit;
        }

        public string Name { get; private set; }
        public string Suit { get; private set; }

    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void ReceiveCard(Card card)
        {
            _cards.Add(card);
        }

        public void ShowCards()
        {
            Console.WriteLine("\nКарты на руках у игрока:");

            foreach (Card card in _cards)
            {
                Console.WriteLine($"{card.Name} - {card.Suit}");
            }
        }
    }
}