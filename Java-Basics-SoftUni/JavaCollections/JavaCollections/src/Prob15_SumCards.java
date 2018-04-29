import java.util.Scanner;

public class Prob15_SumCards {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);		
		String[] cards = input.nextLine().split(" ");
		int sum = 0;
		
		for (int i = 0; i < cards.length; i++) {
			int counter = 1;
			int numFace = returnNum(cards[i]);
			for (int j = i+1; j < cards.length; j++) {
				int currFace = returnNum(cards[j]);
				if (currFace == numFace) {
					counter++;					
				} else break;
			}
			if (counter>1) {
				i+=counter-1;
				counter *=2;				
				sum+=numFace*counter;
			} else sum+=numFace;
		}
		System.out.println(sum);
	}

	private static int returnNum(String string) {
		String face = string.substring(0,string.length()-1);
		switch (face) {
		case "J": return 12; 
		case "Q": return 13; 
		case "K": return 14; 
		case "A": return 15; 
		default: return Integer.parseInt(face);
		}		
	}
}
