

import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/views/doctor_search_view.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/widgets/search_filter_listview.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/widgets/search_history_listview.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/widgets/search_text_field.dart';
import 'package:heal_link/generated/l10n.dart';

class SearchHistorySection extends StatefulWidget {
  const SearchHistorySection({super.key});

  @override
  State<SearchHistorySection> createState() => _SearchHistorySectionState();
}

class _SearchHistorySectionState extends State<SearchHistorySection> {
  final TextEditingController _searchController = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  bool showHistory = false;

  final List<String> searchHistory = [
    'Ahmed Mohamed',
    'Mohamed Samy',
    'Ahmed Hassan',
    'Ahmed Omar',
    'Ahmed Mohamed',
    'Ahmed Ali',
    'Ahmed Alaa',
    'Ahmed Ibrahiem',
    "Mohammed Ali",
    "Gaber Ali",
    "Ahmed Gaber",
    "Mohamed Gaber",
    "Ali Ahmed",
    "Omar Ahmed",
    "Hassan Ali",
    "Samy Mohamed",
  ];

  List<String> filteredResults = [];
  bool showSuggestions = false;

  void filterSearchResults(String query) {
    final results =
        searchHistory
            .where((item) => item.toLowerCase().contains(query.toLowerCase()))
            .toList();

    setState(() {
      filteredResults = results;
      showSuggestions = query.isNotEmpty;
      showHistory = query.isEmpty && _focusNode.hasFocus;
    });
  }

  @override
  void initState() {
    super.initState();

    _focusNode.addListener(() {
      setState(() {
        showHistory = _focusNode.hasFocus && _searchController.text.isEmpty;
        showSuggestions = _searchController.text.isNotEmpty;
        if (_searchController.text.isNotEmpty) {
          filterSearchResults(_searchController.text);
        }
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        SearhTextField(
          searchController: _searchController,
          focusNode: _focusNode,
          searchOnChanged: (value) {
            filterSearchResults(value);
          },
        ),
        SizedBox(height: 16),

        /// Search History Title
        if (showHistory)
          Text(
            S.of(context).search_history,
            style: AppTextStyles.popins500style16PrimaryColor.copyWith(
              color: AppColors.kBlackColor,
            ),
          ),

        const SizedBox(height: 8),

        /// History or Suggestions List 
        Expanded(
          child:
              showHistory
                  ? SearchHistoryListView(searchHistory: searchHistory)
                  : showSuggestions
                  ? SearchFliterListView(
                    filteredResults: filteredResults,
                    searchController: _searchController,
                  )
                  : SizedBox(),
        ),
      ],
    );
  }
}


