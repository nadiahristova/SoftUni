import java.util.Scanner;


public class BeerCounter {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String info = input.nextLine().trim();
		int stacks = 0;
		int beers = 0;
		while (!info.equals("End")) {
			String[] sorter = info.split("\\s+");
			if (sorter[1].equals("beers")) {
				beers += Integer.parseInt(sorter[0]);
			} else
			if (sorter[1].equals("stacks")) {
				stacks +=Integer.parseInt(sorter[0]);
			}
			info = input.nextLine();
		}
		
		stacks += beers/20;
		beers %= 20;
		System.out.printf("%d stacks + %d beers",stacks,beers);
	}

}
