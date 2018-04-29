
public class Prob3_FullHouse {
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
					counter += addSuitsToFacesAndPrint(fullHouseFace, leftSideCount);
				}
			}
		System.out.println();
		System.out.printf("There are total of << %d >> full houses in a deck of cards.", counter);
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
									for(String card:fullHouse){
										System.out.print(card + " ");			
									}
									System.out.println();
									count++;									
							}
						}								
				}
			}
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
