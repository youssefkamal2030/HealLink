import 'package:flutter/material.dart';

import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';
import '../../../../../../generated/l10n.dart';

class AvailableRow extends StatelessWidget {
  const AvailableRow({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      spacing: 20,
      children: [
        Text(
          S.of(context).available_now,
          style: AppTextStyles.popins500style18LightBlackColor
              .copyWith(fontSize: 12, color: AppColors.kPrimaryColor),
        ),
        SizedBox(
          height: 15,
          width: 38,
          child: Transform.scale(
            scaleY: 0.7,
            scaleX: 1.1,
            child: Switch(
              value: true,
              onChanged: (value) {},
              activeColor: Color(0xff48B158),
              activeTrackColor: AppColors.kWhiteColor,
            ),
          ),
        ),
        SizedBox(width: 0),
      ],
    );
  }
}
