import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;
import java.util.TreeMap;


public class Probl15_StraightFlush {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line =input.nextLine().trim();
		String[] cards = line.trim().split("\\W+");
		TreeMap<Character,ArrayList<Integer>> orderedCards = new TreeMap<>();	
		
		for (int i = 0; i < cards.length; i++) {
			char ch =cards[i].charAt(cards[i].length()-1);
			String faceStr = cards[i].substring(0, cards[i].length()-1);
			int face = returnsFace(faceStr);
			if (!orderedCards.containsKey(ch)) {
				orderedCards.put(ch,new ArrayList<Integer>());
			}
			orderedCards.get(ch).add(face);
		}
		
		boolean hasStraightFlushes = false;
		for (char key:orderedCards.keySet()) {
			int listSize = orderedCards.get(key).size();
			if (listSize>4) {				
				Collections.sort(orderedCards.get(key));								
				for (int i = 0; i < listSize-4; i++) {
					int counter=0;
					int currFace = orderedCards.get(key).get(i);
					String flush = "[" + returnsStringFace(currFace) + key + ", ";
					for (int j = i+1; j < i+5; j++) {
						if (!(currFace == orderedCards.get(key).get(j)-1)) {
							break;
						}
						currFace = orderedCards.get(key).get(j);
						counter++;
						if (counter ==4) {
							flush = flush + returnsStringFace(currFace) + key + "]";
							System.out.println(flush);
							hasStraightFlushes = true;
						}else flush = flush + returnsStringFace(currFace) + key + ", ";
					}					
				}
			}
		}

		if (!hasStraightFlushes) {
			System.out.println("No Straight Flushes");
		}
	}

	private static int returnsFace(String str) {		
		switch (str) {
		case "A": return 14;
		case "J": return 11;
		case "Q": return 12;
		case "K": return 13;
		default: return Integer.parseInt(str);}			
		}
		
	private static String returnsStringFace(Integer face) {		
		switch (face) {
		case 14: return "A";
		case 11: return "J";
		case 12: return "Q";
		case 13: return "K";
		default: return Integer.toString(face);			
		}
	}

}
