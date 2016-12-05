using System;

public class Candy
{
	int cValue;
	bool isChecked;
	int location;
	public Candy()
	{
		this.cValue = 0;
		this.isChecked = false;
		this.location = 0;
	}

	public bool isClicked()
	{
		return this.isChecked;
	}

	public void setClicked()
	{
		this.isChecked = true;
	}

	public void setNotClicked()
	{
		this.isChecked = false;
	}

	public int showLocation()
	{
		return this.location;
	}

	public void setLocation(int loc)
	{
		this.location = loc;
	}

	public void setValue(int val)
	{
		this.cValue = val;
	}

	public int getValue()
	{
		return this.cValue;
	}
} //class candy

public class Board
{
	static int candysize = 64;
	Candy[] newBoard = new Candy[candysize];
	public Board()
	{
		for (int i = 0; i < candysize; i++)
		{
			newBoard[i] = new Candy();
			newBoard[i].setValue(0);
			newBoard[i].setNotClicked();
		}
	}

	public int getBoardSize()
	{
		return candysize;
	}

	public Candy getCandy(int i)
	{
		return newBoard[i];
	}
} //class Board

public class program
{
	public void Main()
	{
		board newboard = new Board();
		Console.WriteLine(newboard.getCandy(0).isClicked());
		newboard.getCandy(0).setClicked();
		Console.WriteLine(newboard.getCandy(0).isClicked());
		for (int i = 0; i < newboard.getBoardSize(); i++)
		{
			Console.Write(newboard.getCandy(i).getValue() + " ");
		}

		Console.WriteLine();
		for (int i = 0; i < newboard.getBoardSize(); i++)
		{
			if (newboard.getCandy(i).isClicked())
			{
				Console.Write("1 ");
			}
			else
			{
				Console.Write("0 ");
			}
		}
	} //main()
} //class program
