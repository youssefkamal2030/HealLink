
import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/widgets/search_history_item.dart';

class SearchHistoryListView extends StatelessWidget {
  const SearchHistoryListView({super.key, required this.searchHistory});

  final List<String> searchHistory;
  @override
  Widget build(BuildContext context) {
    return ListView.separated(
      padding: EdgeInsets.symmetric(vertical: 8),
      itemCount: searchHistory.length,
      separatorBuilder: (_, __) => const SizedBox(height: 10),
      itemBuilder: (context, index) {
        return SearchHistoryItem(searchHistory: searchHistory, index: index);
      },
    );
  }
}
