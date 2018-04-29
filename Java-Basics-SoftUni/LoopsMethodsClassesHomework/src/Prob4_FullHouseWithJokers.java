import java.util.Arrays;


public class Prob4_FullHouseWithJokers {
	public static void main(String[] args) {	
		int counter = 0;
		int leftSideCount = 3;		
		String[] fullHouseFace = new String[5];
		for (int faceLeft = 1; faceLeft < 14; faceLeft++)
			for (int faceRight = 1; faceRight < 14; faceRight++) {
				if (!(faceLeft==faceRight)) {				
					for (int k = 0; k < leftSideCount; k++) {
						fullHouseFace[k] = face(faceLeft);
					}
					for (int l = leftSideCount; l < fullHouseFace.length; l++) {
						fullHouseFace[l] = face(faceRight);
					}
					counter+= addSuitsToFacesAndPrint(fullHouseFace, leftSideCount);
				}
		}
		counter+=printHackersFullHand();
		System.out.println();
		System.out.printf("There are total of << %d >> full houses in a deck of cards, if we have Jokers involved in the process.", counter);
	}

	private static int printHackersFullHand() {
		//prints a Full Hand with only Jokers
		String[] hand = {"JK","JK","JK","JK","JK"};
		Arrays.asList(hand).stream().forEach(x -> System.out.print(x + " "));
		System.out.println();
		return 1;
	}

	private static int addSuitsToFacesAndPrint(String[] fullHouseFace, int leftSideCount) {
		int count = 0;
		String suits = "\u2665\u2666\u2663\u2660";
		char[] fullHouseSuits = new char[fullHouseFace.length];
		for (int suit1 = 0; suit1 < 4; suit1++) {
			for (int suit2 = suit1+1; suit2 < 4; suit2++) {
				for (int suit3 = suit2+1; suit3 < 4; suit3++) {					
						fullHouseSuits[0] = suits.charAt(suit1);
						fullHouseSuits[1] = suits.charAt(suit2);
						fullHouseSuits[2] = suits.charAt(suit3);
						for (int suit4 = 0; suit4 < 4; suit4++) {
							for (int suit5 = suit4+1; suit5 < 4; suit5++) {								
									fullHouseSuits[3] = suits.charAt(suit4);
									fullHouseSuits[4] = suits.charAt(suit5);
									String[] fullHouse = new String[fullHouseFace.length];
									for (int card = 0; card < fullHouse.length; card++) {
										fullHouse[card] = fullHouseFace[card] + fullHouseSuits[card] ;
									}																	
									count+= printFullHandWithJokers(fullHouse);
							}
						}								
				}
			}
		}
		return count;
	}

	private static int printFullHandWithJokers(String[] fullHouse) {
		int count = 0;
		int length = (int) Math.pow(2, fullHouse.length);	
		//11111 in decimal representation is 32 or 2^5 (in our case variable length)
		//we don't want the combination of 5 jokers for every fullHouse so we are counting from 0 to 31
		//for 00000(or i = 0) the hand has no jokers; in a hand represented with 00100(or i = 4 ) we have 1 joker at the middle
		for (int i = 0; i < length-1; i++) {
			String[] currFullHouse = fullHouse.clone();
			for (int index = 0; index < fullHouse.length; index++) {
				int num = (i>>index) & 1;
				if (num == 1) {
					currFullHouse[index] = "JK";
				}
			}
			for(String card:currFullHouse){
				System.out.print(card + " ");			
			}
			System.out.println();
			count++;
		}		
		return count;
	}

	private static String face(int i) {
		String currFace = null;
		switch (i) {
		case 1: currFace = "A";	break;
		case 11: currFace = "J"; break;
		case 12: currFace = "Q"; break;
		case 13: currFace = "K"; break;
		default: currFace = Integer.toString(i); break;
		}
		return currFace;
	}
}
