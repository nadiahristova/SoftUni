import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;


public class Prob6_RandomHandsOf5Cards {
	private final static int cardCount = 5;
	private static ArrayList<String> deck = generateDeckOfCards();
	
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		Random rnd = new Random();
		
		int numHands = input.nextInt();
		//deck = generateDeckOfCards();
		for (int i = 0; i < numHands; i++) {
			for (int card = 0; card < cardCount; card++) {
				int rndIndex = rnd.nextInt(deck.size());
				System.out.print(deck.get(rndIndex) + " ");
				deck.remove(rndIndex);
			}		
			System.out.println();
		}
	}

	private static ArrayList<String> generateDeckOfCards() {
		ArrayList<String> dCards = new ArrayList<>();
		String suits = "\u2665\u2666\u2663\u2660";
		String[] faces = {"A","2","3","4","5","6","7","8","9","10","J","Q","K"};
		for (int faceIndex = 0; faceIndex < faces.length; faceIndex++) {
			for (int suitIndex = 0; suitIndex < suits.length(); suitIndex++) {
				dCards.add(faces[faceIndex]+suits.charAt(suitIndex));
			}
		}
		return dCards;
	}
}
