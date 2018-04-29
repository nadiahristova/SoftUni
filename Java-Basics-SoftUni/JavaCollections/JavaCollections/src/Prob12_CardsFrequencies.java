import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.Scanner;

public class Prob12_CardsFrequencies {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		String line = input.nextLine().trim();
		String[] cards = line.trim().split("\\s+");		
		HashMap<String,Integer> facesAndOccur = new LinkedHashMap<>();
		
		for (int i = 0; i < cards.length-1; i++) {
			if (!cards[i].equals("X")) {
				String face = cards[i].substring(0,cards[i].length()-1);
				int numOccur = 1;				
				for (int j = i+1; j < cards.length; j++) {
					String currFace = cards[j].substring(0,cards[j].length()-1);
					if (face.equals(currFace)) {
						cards[j] = "X";
						numOccur++;
					}
				}	
				facesAndOccur.put(face, numOccur);
			}			
		}
		
		for (String face : facesAndOccur.keySet()) {
			double percentage = facesAndOccur.get(face)*100.0/cards.length;
			System.out.println(String.format("%s -> %.2f%%", face, percentage));
		}
		
	}
}
