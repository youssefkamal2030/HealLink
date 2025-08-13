import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:go_router/go_router.dart';

import '../utils/app_images.dart';
import '../utils/app_styles.dart';

class CustomAppBarPopWidget extends StatelessWidget {
  const CustomAppBarPopWidget({super.key, required this.text});

  final String text;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 64),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          GestureDetector(
            onTap: () => context.pop(),
            child: SvgPicture.asset(AppImages.arrowLeftIcon),
          ),
          Spacer(),
          Padding(
            padding: const EdgeInsetsDirectional.only(end: 24),
            child: Text(
              text,
              textAlign: TextAlign.center,
              style: AppTextStyles.popins500style18LightBlackColor.copyWith(
                fontSize: 20,
              ),
            ),
          ),

          Spacer(),
        ],
      ),
    );
  }
}
