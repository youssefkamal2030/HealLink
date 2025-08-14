import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/chronic_diseases_item.dart';
import '../../../../../core/widgets/custom_app_bar_pop_widget.dart';
import '../../../../../generated/l10n.dart';
import '../../data/models/title_and_subtitle_model.dart';

class ChronicDiseasesView extends StatelessWidget {
  const ChronicDiseasesView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.only(
          top: 4.0,
          bottom: 16,
          left: 16,
          right: 16,
        ),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).chronic_diseases),
            SizedBox(height: 24),
            Expanded(
              child: ListView.builder(
                padding: EdgeInsets.zero,
                itemBuilder:
                    (context, index) => Padding(
                      padding: const EdgeInsets.only(bottom: 8.0),
                      child: ChronicDiseasesItemWidget(
                        isNote: true,
                        titleAndSubtitleList: [
                          TitleAndSubtitleModel(
                            title: 'Disease:',
                            subtitle: 'Diabetes Type 2',
                          ),
                          TitleAndSubtitleModel(
                            title: 'Diagnosed Since:',
                            subtitle: 'March 2019',
                          ),
                          TitleAndSubtitleModel(
                            title: 'Notes:',
                            subtitle: 'Patient monitors blood sugar daily',
                          ),
                        ],
                      ),
                    ),
                itemCount: 3,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
