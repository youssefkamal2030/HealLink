import 'package:flutter/material.dart';

import '../../../../../../core/utils/function/app_colors.dart';
import '../../../data/models/title_and_subtitle_model.dart';
import 'chronic_diseases_item_row.dart';

class ChronicDiseasesItemWidget extends StatelessWidget {
  const ChronicDiseasesItemWidget({
    super.key,
    required this.isNote,
    required this.titleAndSubtitleList,
  });

  final bool isNote;
  final List<TitleAndSubtitleModel> titleAndSubtitleList;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(8),
      decoration: ShapeDecoration(
        color: Colors.white,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
      ),
      child: Column(
        spacing: 4,
        children: [
          for (int i = 0; i < titleAndSubtitleList.length; i++)
            ChronicDiseasesItemRow(
              title: titleAndSubtitleList[i].title,
              subTitle: titleAndSubtitleList[i].subtitle,
              titleColor:
                  isNote && i == titleAndSubtitleList.length - 1
                      ? AppColors.kGreenColor
                      : AppColors.kPrimaryColor,
            ),
        ],
      ),
    );
  }
}
