
import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/widgets/search_filter_item.dart';

class SearchFliterListView extends StatelessWidget {
  const SearchFliterListView({
    super.key,
    required this.filteredResults,
    required TextEditingController searchController,
  }) : _searchController = searchController;

  final List<String> filteredResults;
  final TextEditingController _searchController;

  @override
  Widget build(BuildContext context) {
    return filteredResults.isEmpty
        ? const Center(child: Text('No suggestions found.'))
        : ListView.builder(
          padding: EdgeInsets.zero,
          itemCount: filteredResults.length,
          itemBuilder: (context, index) {
            final item = filteredResults[index];
            return SearchFilterItem(item: item, searchController: _searchController);
          },
        );
  }
}
