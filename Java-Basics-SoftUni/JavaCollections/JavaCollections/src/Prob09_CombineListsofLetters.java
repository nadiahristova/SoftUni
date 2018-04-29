import java.util.*;


public class Prob09_CombineListsofLetters {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String list1 = input.nextLine().trim();
		String list2 = input.nextLine().trim();
		String[] strArrList1 = list1.trim().split("\\s+");		
		String[] strArrList2 = list2.trim().split(" ");
		
		ArrayList<String> joinedList = new ArrayList<>();		
		ArrayList<String> arrList2 = new ArrayList<>();
		ArrayList<String> middleList = new ArrayList<>();
		
		joinedList.addAll(Arrays.asList(strArrList1));
		arrList2.addAll(Arrays.asList(strArrList2));
		middleList.addAll(Arrays.asList(strArrList2));		
		
		arrList2.retainAll(joinedList);
		middleList.removeAll(arrList2);
		joinedList.addAll(middleList);
		
		for (String letter:joinedList) {
			System.out.print(letter + " ");
		}
	}	
}
