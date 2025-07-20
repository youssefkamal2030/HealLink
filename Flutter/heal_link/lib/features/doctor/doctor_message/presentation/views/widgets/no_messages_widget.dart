import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/generated/l10n.dart';

class NoMessagesWidget extends StatelessWidget {
  const NoMessagesWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.start,
      children: [
        const SizedBox(height: 50),
        SvgPicture.asset(AppImages.noMessage),
        const SizedBox(height: 20),
        Text(
          S.of(context).no_chat_at_moment,
          style: AppTextStyles.popins400style16LightDarkGrey.copyWith(
            color: AppColors.kPrimaryColor,
          ),
        ),
        const SizedBox(height: 10),
        Text(
          textAlign: TextAlign.center,
          S.of(context).once_patients_chatting,
          style: AppTextStyles.popins400style12LightBlackColor,
        ),
      ],
    );
  }
}
