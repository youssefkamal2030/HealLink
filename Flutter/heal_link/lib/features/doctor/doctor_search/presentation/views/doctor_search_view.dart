import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/widgets/search_histoy_section.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSearchView extends StatelessWidget {
  const DoctorSearchView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: EdgeInsets.symmetric(horizontal: 16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            CustomAppBarPopWidget(text: S.of(context).search),
            SizedBox(height: 16),
            Expanded(child: SearchHistorySection()),
          ],
        ),
      ),
    );
  }
}








