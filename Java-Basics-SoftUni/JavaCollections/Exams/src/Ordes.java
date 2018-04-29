import java.security.KeyStore.Entry;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;


public class Ordes {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int n = Integer.parseInt(input.nextLine());
		Map<String, TreeMap<String,Integer>> out = new LinkedHashMap<>();
		
		for (int i = 0; i < n; i++) {
			String[] in = input.nextLine().split(" ");
			String name = in[0];
			int order = Integer.parseInt(in[1]);
			String prod = in[2];

			if (!out.containsKey(prod)) {
				out.put(prod, new TreeMap<String, Integer>());
			}
			if (!out.get(prod).containsKey(name)) {
				out.get(prod).put(name, order);
			} else {
				int oldOSum = out.get(prod).get(name);
				out.get(prod).put(name, order + oldOSum);
			}
		}
		for (String product : out.keySet()) {
			System.out.print(product + ": ");
			TreeMap<String, Integer> amounts = out.get(product);
			boolean zapetajka = true;
			for (Map.Entry<String, Integer> pair : amounts.entrySet()) {
				if (!zapetajka) {
					System.out.print(", ");
				}
				String user = pair.getKey();
				int am = pair.getValue();
				System.out.print(user + " " + am);
				zapetajka = false;
			}
			System.out.println();
		}
	}
}
